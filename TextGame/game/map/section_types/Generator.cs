using System;

namespace TextGame.game.map.section_types {
    public class Generator : IMapSection {
        public bool Activated { get; set; }
        public bool ContainsPlayer { get; set; }
        public Item Item { get; set; }
        public string Name { get; set; } = "Generator";

        public void Options() {
            if (this.Activated) {
                Console.WriteLine("You are stood by an active generator.");
            }
            else {
                Console.WriteLine("You are stood by a generator, which you can activate.");
            }
        }

        public void ActivateGenerator() {
            if (this.Activated) {
                Console.WriteLine("This generator is already running...");
            }
            else {
                this.Activated = true;
                Console.WriteLine("You hear a soft hum, and lights blink on. The generator is now running.");
            }
        }
    }
}