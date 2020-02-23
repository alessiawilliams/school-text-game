using System.Dynamic;

namespace TextGame.game
{
    public class Player
    {
        // The player will have a tracked location and an inventory of 3 slots
        public int[] Location = new int[2] { 0, 0 };
        public Item[] Inventory = new Item[3];

        public void FixGenerator()
        {
            
        }
    }
}