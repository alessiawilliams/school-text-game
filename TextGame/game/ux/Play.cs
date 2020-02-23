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
            // TODO implement options for each type of section
            // TODO implement movement
            //ShowMap(g.Player.Location, g.Map.Layout);
            string input = AcceptInput();
            ParseInput(input);
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
            int pY = l[0];
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

        private static void ParseInput(string input)
        {
            
        }
    }
}