using System;
using System.IO;
using System.Threading;
using TextGame.game;
using TextGame.game.ux;

namespace TextGame
{
    internal static class Init
    {
        public static void Main(string[] args)
        {
            RunMenu();
            GameInstance g = new GameInstance();
            Play.PlayGame(g);
        }

        private static void RunMenu()
        {
            using (StreamReader s = new StreamReader("menutext.txt"))
            {
                string t;
                while ((t = s.ReadLine()) != null)
                {
                    foreach (char c in t)
                    {
                        Console.Write(c);
                        Thread.Sleep(5);
                    }
                    Console.WriteLine();
                }
            }
            Console.ReadKey();
        }
    }
}