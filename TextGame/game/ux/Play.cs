using System;
using System.Threading;
using TextGame.game.map;
using TextGame.game.map.section_types;
using TextGame.game.ux.util;

namespace TextGame.game.ux {
    public static class Play {
        public static void PlayGame(GameInstance game) {
            Console.Clear();
            LookAround(game.Player.Location, game.Map.Matrix);
            ShowMap(game.Player.Location, game.Map.Matrix);
            // Console.WriteLine($"{game.Player.Location[0]}, {game.Player.Location[1]}");
            CurrentOptions(game.Player.Location, game.Map.Matrix);
            // TODO: Implement options for each type of section - hatch: winning, opening - gens: unlucky
            string input = AcceptInput();
            ParseInput(input.ToLower(), game);
            PlayGame(game);
        }

        private static void ShowMap(int[] location, IMapSection[,] map) {
            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    if (location[0] == i && location[1] == j) {
                        Console.Write("X\t");
                    }
                    else if (map[i, j] is Grassland) {
                        Console.Write("-\t");
                    }
                    else if (map[i, j] is Hatch) {
                        Console.Write("h\t");
                    }
                    else if (map[i, j] is Tree) {
                        Console.Write("t\t");
                    }
                    else if (map[i, j] is Generator) {
                        Console.Write("g\t");
                    }
                }

                Console.WriteLine();
            }
        }

        private static void LookAround(int[] location, IMapSection[,] map) {
            int pY = location[0];
            int pX = location[1];

            try {
                if (!(map[pY - 1, pX] is Grassland)) {
                    SlowPrint.Print($"North of you, you see a {map[pY - 1, pX].Name}");
                }
            }
            catch {
                SlowPrint.Print("You cannot see further north...");
            }

            try {
                if (!(map[pY + 1, pX] is Grassland)) {
                    SlowPrint.Print($"South of you, there is a {map[pY + 1, pX].Name}");
                }
            }
            catch {
                SlowPrint.Print("You cannot see further south...");
            }

            try {
                if (!(map[pY, pX + 1] is Grassland)) {
                    SlowPrint.Print($"East of you, there is a {map[pY, pX + 1].Name}");
                }
            }
            catch {
                SlowPrint.Print("You cannot see further east...");
            }

            try {
                if (!(map[pY, pX - 1] is Grassland)) {
                    SlowPrint.Print($"West of you, there is a {map[pY, pX - 1].Name}");
                }
            }
            catch {
                SlowPrint.Print("You cannot see further west...");
            }
        }

        private static void CurrentOptions(int[] location, IMapSection[,] map) {
            if (!(map[location[0], location[1]] is Grassland)) {
                map[location[0], location[1]].Options();
            }
        }

        private static string AcceptInput() {
            Console.WriteLine();
            Console.Write("> ");
            return Console.ReadLine();
        }

        private static void ParseInput(string input, GameInstance game) {
            // Inventory management/items
            if (input.Contains("inventory") || input.Contains("bag")) {
                game.Player.ViewInventory();
            }
            else if (input.Contains("map") || input.Contains("flashlight")) {
                int useItem = game.Player.UseItem(input.Contains("map") ? "Map" : "Flashlight");
                if (useItem % 2 == 0) {
                    SlowPrint.Print("You don't have the required item for that.");
                }
                else {
                    if (useItem == 1) {
                        ShowMap(game.Player.Location, game.Map.Matrix);
                    }
                    else if (useItem == 3) {
                        // TODO: Create flashlight mechanics.
                    }
                }
            }

            // Movement
            if (input.Contains("north")) {
                game.Player.Move(0);
                SlowPrint.Print("You headed north.");
            }
            else if (input.Contains("south")) {
                game.Player.Move(1);
                SlowPrint.Print("You ventured south.");
            }
            else if (input.Contains("east")) {
                game.Player.Move(2);
                SlowPrint.Print("You moved east.");
            }
            else if (input.Contains("west")) {
                game.Player.Move(3);
                SlowPrint.Print("You went west.");
            }

            // World actions
            if (input.Contains("generator") || input.Contains("fix") || input.Contains("activate")) {
                if (game.Map.Matrix[game.Player.Location[0], game.Player.Location[1]] is Generator) {
                    /* I don't like downcasting like this, but without implementing a blank method in each class
                        it's the easiest method. For safety, this checks if the instance is Generator anyway!
                        Note to self in the future: C# 8.0 has default implementations in an interface. */
                    ((Generator) game.Map.Matrix[game.Player.Location[0], game.Player.Location[1]]).ActivateGenerator();
                    game.Map.ActiveGenerators += 1;
                }
                else {
                    SlowPrint.Print("You aren't near a generator at the moment. Try to find one.");
                }
            }

            if (input.Contains("tree") || input.Contains("search") || input.Contains("item")) {
                if (game.Map.Matrix[game.Player.Location[0], game.Player.Location[1]].Item != null) {
                    if (game.Player.ItemsHeld < 3) {
                        for (int i = 0; i < 2; i++) {
                            game.Player.Inventory[i] =
                                game.Map.Matrix[game.Player.Location[0], game.Player.Location[1]].Item;
                        }

                        game.Player.ItemsHeld += 1;
                    }
                    else {
                        SlowPrint.Print("Your bag is already full.");
                    }
                }
                else {
                    SlowPrint.Print("You can't find any items here. Try a tree.");
                }
            }

            if (input.Contains("drop")) {
                if (game.Map.Matrix[game.Player.Location[0], game.Player.Location[1]].Item == null) {
                    SlowPrint.Print("Which item would you like to drop?");
                    string i = Console.ReadLine();
                    if (i == "0" || i == "1" || i == "2") {
                        game.Map.Matrix[game.Player.Location[0], game.Player.Location[1]].Item =
                            game.Player.Inventory[int.Parse(i)];
                    }
                    else {
                        SlowPrint.Print("You chose not to drop anything.");
                    }
                }
                else {
                    SlowPrint.Print("There's already an item on the ground here.");
                }
            }

            if (input.Contains("hatch") || input.Contains("escape")) {
                if (game.Map.Matrix[game.Player.Location[0], game.Player.Location[1]] is Hatch) {
                    if (game.Map.ActiveGenerators == 4) {
                        SlowPrint.Print("You flick the switch, and the hatch slowly slides open...");
                        Thread.Sleep(100);
                        Console.Clear();
                        Win.WinSequence();
                    }
                    else {
                        SlowPrint.Print("The hatch isn't powered and can't currently be opened.");
                    }
                }
            }
        }
    }
}