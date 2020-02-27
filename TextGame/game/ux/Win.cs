using System;
using TextGame.game.ux.util;

namespace TextGame.game.ux {
    public class Win {
        public static void WinSequence() {
            SlowPrint.Print("Congratulations! You have managed to escape the perilous forest with your life.");
            Console.ReadKey();
        }
    }
}