using System;
using TextGame.section_types;

namespace TextGame.game.ux
{
    public static class Play
    {
        public static void PlayGame(GameInstance g)
        {
            Console.WriteLine("You are stranded in the dark park. You have nothing in your bag.");
            LookAround(g.Player.Location, g.Map.Layout);
            CurrentOptions(g.Player.Location, g.Map.Layout);
            // TODO: implement options for each type of section
            string input = AcceptInput();
            ParseInput(input.ToLower(), g);
        }

        private static void ShowMap(int[] l, MapSection[,] m)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (l[0] == i && l[1] == j)
                    {
                        Console.Write("X\t");
                    }
                    else if (m[i, j] is Grassland)
                    {
                        Console.Write("-\t");
                    }
                    else if (m[i, j] is Hatch)
                    {
                        Console.Write("h\t");
                    }
                    else if (m[i, j] is Tree)
                    {
                        Console.Write("t\t");
                    }
                    else if (m[i, j] is Generator)
                    {
                        Console.Write("g\t");
                    }
                }
                Console.WriteLine();
            }
        }

        private static void LookAround(int[] l, MapSection[,] m)
        {
            //TODO: make this cleanly not prone to IndexOutOfRangeException
            int pX = l[0];
            int pY = l[1];
            if (!(m[pX, pY + 1] is Grassland))
            {
                Console.WriteLine($"North of you, you see a {m[pX, pY + 1].Name}");
            }
            if (!(m[pX, pY - 1] is Grassland))
            {
                Console.WriteLine($"South of you, there is a {m[pX, pY - 1].Name}");
            }
            if (!(m[pX + 1, pY] is Grassland))
            {
                Console.WriteLine($"East of you, there is a {m[pX + 1, pY].Name}");
            }
            if (!(m[pX - 1, pY] is Grassland))
            {
                Console.WriteLine($"West of you, there is a {m[pX- 1, pY].Name}");
            }
        }

        private static void CurrentOptions(int[] l, MapSection[,] m)
        {
            if (!(m[l[0], l[1]] is Grassland))
            {
                m[l[0], l[1]].Options();
            }
        }

        private static string AcceptInput()
        {
            Console.WriteLine();
            Console.Write("> ");
            return Console.ReadLine();
        }

        private static void ParseInput(string input, GameInstance g)
        {
            // Inventory management/items
            if (input.Contains("inventory") || input.Contains("bag"))
            {
                g.Player.ViewInventory();
            }
            else if (input.Contains("map"))
            {
                ShowMap(g.Player.Location, g.Map.Layout);
            }
            else if (input.Contains("flashlight"))
            {
                // TODO: Implement flashlight mechanism
            }

            // Movement
            if (input.Contains("north"))
            {
                g.Player.Move(0);
                Console.WriteLine("You headed north.");
            }
            else if (input.Contains("south"))
            {
                g.Player.Move(1);
                Console.WriteLine("You ventured south.");
            }
            else if (input.Contains("east"))
            {
                g.Player.Move(2);
                Console.WriteLine("You moved east.");
            }
            else if (input.Contains("west"))
            {
                g.Player.Move(3);
                Console.WriteLine("You went west.");
            }

            // World actions
            if (input.Contains("generator") || input.Contains("fix"))
            {
                if (g.Map.Layout[g.Player.Location[0], g.Player.Location[1]] is Generator)
                {
                    g.Map.Layout[g.Player.Location[0], g.Player.Location[1]].Activate();
                }
                else
                {
                    Console.WriteLine("You aren't near a generator at the moment. Try to find one.");
                }
            }
            if (input.Contains("drop"))
            {
                Console.WriteLine("Which item would you like to drop?");
                string i = Console.ReadLine();
                if (i == "0" || i == "1" || i == "2")
                {
                    g.Map.Layout[g.Player.Location[0], g.Player.Location[1]].Item = g.Player.Inventory[int(i)];
                }
                else
                {
                    Console.WriteLine("You chose not to drop anything.");
                }
            }
        }
    }
}