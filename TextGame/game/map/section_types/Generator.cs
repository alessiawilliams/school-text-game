using System;

namespace TextGame.section_types
{
    public class Generator : MapSection
    {
        public bool ContainsPlayer { get; set; }
        public Item Item { get; set; }
        public bool Activated { get; set; }
        public string Name { get; set; } = "Generator";

        public void Activate()
        {
            if (this.Activated == true)
            {
                Console.WriteLine("This generator is already running...");
            }
            else
            {
                this.Activated = true;
                Console.WriteLine("You hear a soft hum, and lights blink on. The generator is now running.");
            }
        }

        public void Options()
        {
            if (this.Activated == true)
            {
                Console.WriteLine("You are stood by an active generator.");
            }
            else
            {
                Console.WriteLine("You are stood by a generator, which you can activate.");
            }
        }
    }
}