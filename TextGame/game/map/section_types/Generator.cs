using TextGame.game.ux;
using TextGame.game.ux.util;

namespace TextGame.game.map.section_types {
    public class Generator : IMapSection {
        public bool Activated { get; set; }
        public bool Unlucky { get; set; }
        public bool ContainsPlayer { get; set; }
        public Item Item { get; set; }
        public string Name { get; set; } = "Generator";

        public void Options(int gensActive) {
            if (this.Activated) {
                SlowPrint.Print("You are stood by an active generator.");
            }
            else {
                SlowPrint.Print("You are stood by a generator, which you can activate.");
            }
        }

        public void ActivateGenerator() {
            if (this.Activated) {
                SlowPrint.Print("This generator is already running...");
            }
            else if (!this.Unlucky) {
                this.Activated = true;
                SlowPrint.Print("You hear a soft hum, and lights blink on. The generator is now running.");
            }
            else {
                Lose.LoseSequence();
            }
        }
    }
}