using TextGame.game.ux;
using TextGame.game.ux.util;

namespace TextGame.game.map.section_types {
    public class Hatch : IMapSection {
        public bool ContainsPlayer { get; set; }
        public Item Item { get; set; }
        public string Name { get; set; } = "Hatch";

        public void Options(int gens) {
            if (gens >= 4) {
                SlowPrint.Print("You are stood next to a hatch, which seems to be powered.");
            }
            else {
                SlowPrint.Print("You are stood next to a hatch, which is locked shut, and has no power.");
            }
        }

        public void UseHatch() { 
            SlowPrint.Print("You flick the switch, and the hatch slowly slides open...");
            Thread.Sleep(100);
            Console.Clear();
            Win.WinSequence();
        }
    }
}