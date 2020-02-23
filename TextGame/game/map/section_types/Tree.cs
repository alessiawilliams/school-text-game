using System;

namespace TextGame.section_types
{
    public class Tree : MapSection
    {
        public bool ContainsPlayer { get; set; }
        public Item Item { get; set; }
        public string Name { get; set; } = "Tree";

        public Tree()
        {
            // When a tree is created, it should contain an item - either a flashlight, or a map.
            Random r = new Random();
            if (r.Next(2) == 1)
            {
                this.Item = new Item("flashlight");
            }
            else
            {
                this.Item = new Item("map");
            }
        }
        
        public void GiveItem()
        {
            this.Item = null;
        }

        public void Options()
        {
            
        }
    }
}