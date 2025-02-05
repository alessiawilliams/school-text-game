﻿using System;
using TextGame.game.map;
using TextGame.game.map.section_types;
using TextGame.game.ux.util;

namespace TextGame.game.ux {
    public static class Play {
        public static void PlayGame(GameInstance game) {
            // TODO: Beautify output
            Console.Clear();
            LookAround(game.Player.Location, game.Map.Matrix, game.Player);
            CurrentOptions(game.Player.Location, game.Map.Matrix, game.Map.ActiveGenerators);
            string input = AcceptInput();
            ParseInput(input.ToLower(), game);
            PlayGame(game);
        }

        private static void ShowMap(int[] location, IMapSection[,] map) {
            Console.Clear();
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

            Console.WriteLine("\nX: PLAYER; h: hatch; t: tree; g: generator");
            Console.WriteLine("\nPress RETURN to return to game.");
            Console.ReadKey();
            SlowPrint.Print("The map fell to pieces.");
        }

        private static void LookAround(int[] location, IMapSection[,] map, Player player) {
            int pY = location[0];
            int pX = location[1];

            try {
                if (!(map[pY - 1, pX] is Grassland)) {
                    SlowPrint.Print($"North of you, you see a {map[pY - 1, pX].Name}");
                }

                if (player.FlashActive) {
                    if (!(map[pY - 2, pX] is Grassland)) {
                        SlowPrint.Print($"Far north of you, you see a {map[pY - 2, pX].Name}");
                    }
                }
            }
            catch {
                SlowPrint.Print("You cannot see further north...");
            }

            try {
                if (!(map[pY + 1, pX] is Grassland)) {
                    SlowPrint.Print($"South of you, there is a {map[pY + 1, pX].Name}");
                }

                if (player.FlashActive) {
                    if (!(map[pY + 2, pX] is Grassland)) {
                        SlowPrint.Print($"Far south of you, you see a {map[pY + 2, pX].Name}");
                    }
                }
            }
            catch {
                SlowPrint.Print("You cannot see further south...");
            }

            try {
                if (!(map[pY, pX + 1] is Grassland)) {
                    SlowPrint.Print($"East of you, there is a {map[pY, pX + 1].Name}");
                }

                if (player.FlashActive) {
                    if (!(map[pY, pX + 2] is Grassland)) {
                        SlowPrint.Print($"Far east of you, you see a {map[pY, pX + 2].Name}");
                    }
                }
            }
            catch {
                SlowPrint.Print("You cannot see further east...");
            }

            try {
                if (!(map[pY, pX - 1] is Grassland)) {
                    SlowPrint.Print($"West of you, there is a {map[pY, pX - 1].Name}");
                }

                if (player.FlashActive) {
                    if (!(map[pY, pX - 2] is Grassland)) {
                        SlowPrint.Print($"Far west of you, you see a {map[pY, pX - 2].Name}");
                    }
                }
            }
            catch {
                SlowPrint.Print("You cannot see further west...");
            }

            player.FlashActive = false;
        }

        private static void CurrentOptions(int[] location, IMapSection[,] map, int gensActive) {
            if (!(map[location[0], location[1]] is Grassland)) {
                map[location[0], location[1]].Options(gensActive);
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
                        game.Player.FlashActive = true;
                    }
                }
            }

            // Movement
            if (input.Contains("north")) {
                game.Player.Move(0);
            }
            else if (input.Contains("south")) {
                game.Player.Move(1);
            }
            else if (input.Contains("east")) {
                game.Player.Move(2);
            }
            else if (input.Contains("west")) {
                game.Player.Move(3);
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
                IMapSection tile = game.Map.Matrix[game.Player.Location[0], game.Player.Location[1]];
                
                if (game.Map.Matrix[game.Player.Location[0], game.Player.Location[1]].Item != null) {
                    if (game.Player.ItemsHeld < 3) {
                        for (int i = 0; i < 3; i++) {
                            if (game.Player.Inventory[i] == null) {
                                game.Player.Inventory[i] = tile.Item;
                                SlowPrint.Print($"You found a {tile.Item.Type} in the {tile.Name}!");
                                game.Map.Matrix[game.Player.Location[0], game.Player.Location[1]].Item = null;
                                break;
                            }

                            if (tile is Tree) {
                                ((Tree)game.Map.Matrix[game.Player.Location[0], game.Player.Location[1]]).Searched = true;
                            }
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
                        game.Map.Matrix[game.Player.Location[0], game.Player.Location[1]].Item = game.Player.Inventory[int.Parse(i)];
                        game.Player.Inventory[int.Parse(i)] = null;
                        game.Player.ItemsHeld -= 1;
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
                        ((Hatch) game.Map.Matrix[game.Player.Location[0], game.Player.Location[1]]).UseHatch();
                    }
                    else {
                        SlowPrint.Print("The hatch isn't powered and can't currently be opened.");
                    }
                }
            }
        }
    }
}