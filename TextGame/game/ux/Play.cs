using System;
using TextGame.game.map;
using TextGame.game.map.section_types;

namespace TextGame.game.ux {
    public static class Play {
        public static void PlayGame(GameInstance game) {
            //Console.WriteLine("You are stranded in the dark park. You have nothing in your bag.");
            LookAround(game.Player.Location, game.Map.Matrix);
            Console.WriteLine($"{game.Player.Location[0]}, {game.Player.Location[1]}");
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
            int pX = location[0];
            int pY = location[1];

            try {
                if (!(map[pX, pY + 1] is Grassland)) {
                    Console.WriteLine($"North of you, you see a {map[pX, pY + 1].Name}");
                }
            }
            catch {
                Console.WriteLine("You cannot see further north...");
            }

            try {
                if (!(map[pX, pY - 1] is Grassland)) {
                    Console.WriteLine($"South of you, there is a {map[pX, pY - 1].Name}");
                }
            }
            catch {
                Console.WriteLine("You cannot see further south...");
            }

            try {
                if (!(map[pX + 1, pY] is Grassland)) {
                    Console.WriteLine($"East of you, there is a {map[pX + 1, pY].Name}");
                }
            }
            catch {
                Console.WriteLine("You cannot see further east...");
            }

            try {
                if (!(map[pX - 1, pY] is Grassland)) {
                    Console.WriteLine($"West of you, there is a {map[pX - 1, pY].Name}");
                }
            }
            catch {
                Console.WriteLine("You cannot see further west...");
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
                    Console.WriteLine("You don't have the required item for that.");
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
                Console.WriteLine("You headed north.");
            }
            else if (input.Contains("south")) {
                game.Player.Move(1);
                Console.WriteLine("You ventured south.");
            }
            else if (input.Contains("east")) {
                game.Player.Move(2);
                Console.WriteLine("You moved east.");
            }
            else if (input.Contains("west")) {
                game.Player.Move(3);
                Console.WriteLine("You went west.");
            }

            // World actions
            if (input.Contains("generator") || input.Contains("fix") || input.Contains("activate")) {
                if (game.Map.Matrix[game.Player.Location[0], game.Player.Location[1]] is Generator) {
                    /* I don't like downcasting like this, but without implementing a blank method in each class
                        it's the easiest method. For safety, this checks if the instance is Generator anyway!
                        Note to self in the future: C# 8.0 has default implementations in an interface. */
                    ((Generator) game.Map.Matrix[game.Player.Location[0], game.Player.Location[1]]).ActivateGenerator();
                }
                else {
                    Console.WriteLine("You aren't near a generator at the moment. Try to find one.");
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
                        Console.WriteLine("Your bag is already full.");
                    }
                }
                else {
                    Console.WriteLine("You can't find any items here. Try a tree.");
                }
            }

            if (input.Contains("drop")) {
                if (game.Map.Matrix[game.Player.Location[0], game.Player.Location[1]].Item == null) {
                    Console.WriteLine("Which item would you like to drop?");
                    string i = Console.ReadLine();
                    if (i == "0" || i == "1" || i == "2") {
                        game.Map.Matrix[game.Player.Location[0], game.Player.Location[1]].Item =
                            game.Player.Inventory[int.Parse(i)];
                    }
                    else {
                        Console.WriteLine("You chose not to drop anything.");
                    }
                }
                else {
                    Console.WriteLine("There's already an item on the ground here.");
                }
            }
        }
    }
}