using System;
using System.IO;
using System.Threading;
using TextGame.game;
using TextGame.game.ux;

namespace TextGame {
    internal static class Init {
        public static void Main(string[] args) {
            RunMenu();
            Play.PlayGame(new GameInstance());
        }

        private static void RunMenu() {
            using (StreamReader s = new StreamReader("menutext.txt")) {
                string t;
                while ((t = s.ReadLine()) != null) {
                    foreach (char c in t) {
                        Console.Write(c);
                        Thread.Sleep(1);
                    }

                    Console.WriteLine();
                }
            }

            Console.ReadKey();
        }
    }
}