using System;
using System.Threading;

namespace TextGame.game.ux.util {
    public abstract class SlowPrint {
        public static void Print(string str, int sleep = 50, bool newLine = true) {
            foreach (char c in str) {
                Console.Write(c);
                Thread.Sleep(sleep);
            }

            if (newLine) {
                Console.WriteLine();
            }
        }
    }
}