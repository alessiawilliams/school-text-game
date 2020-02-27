using System;
using TextGame.game.ux.util;

namespace TextGame.game.ux {
    public class Lose {
        public static void LoseSequence() {
            Console.Clear();
            SlowPrint.Print("You begin to fiddle with the generator, listening closely for a faint hum.");
            SlowPrint.Print("...", 1000);
            Console.WriteLine("BANG!");
            SlowPrint.Print("\nYou died to an explosion, caused by the old generator.");
            SlowPrint.Print("\nPress RETURN to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}