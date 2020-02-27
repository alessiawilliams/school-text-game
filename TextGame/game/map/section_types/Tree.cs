using System;
using TextGame.game.ux.util;

namespace TextGame.game.map.section_types {
    public class Tree : IMapSection {
        public Tree() {
            // When a tree is created, it should contain an item - either a flashlight, or a map.
            Random r = new Random();
            this.Item = r.Next(2) == 1 ? new Item("Flashlight") : new Item("Map");
        }

        public bool ContainsPlayer { get; set; }
        public Item Item { get; set; }
        public string Name { get; set; } = "Tree";
        public bool Searched { get; set; } = false;

        public void Options(int gensActive) {
            if (!this.Searched) {
                SlowPrint.Print("You are stood by a tree, which you can search.");
            }
            else {
                Console.WriteLine("You are stood by a tree, which you have searched before.");
            }
        }
    }
}