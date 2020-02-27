using System;
using TextGame.game.ux.util;

namespace TextGame.game.ux {
    public class Win {
        public static void WinSequence() {
            SlowPrint.Print("Congratulations! You have managed to escape the perilous forest with your life.");
            SlowPrint.Print("Press RETURN to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}