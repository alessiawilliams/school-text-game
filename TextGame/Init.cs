using System;
using System.IO;
using TextGame.game;
using TextGame.game.ux;
using TextGame.game.ux.util;

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
                    SlowPrint.Print(t);
                }
            }

            Console.ReadKey();
        }
    }
}