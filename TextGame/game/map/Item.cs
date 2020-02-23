using System;

namespace TextGame
{
    public class Item
    {
        public string Type { get; set; }

        public Item(string type)
        {
            this.Type = type;
        }
    }
}