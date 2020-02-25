using System.Dynamic;

namespace TextGame.game
{
    public class Player
    {
        // The player will have a tracked location and an inventory of 3 slots
        public int[] Location = new int[2] { 0, 0 };
        public Item[] Inventory = new Item[3];

        public void Move(int dir)
        {
            if (dir == 0) // north
            {
                this.Location[1] += 1
            }
            else if (dir == 1) // south
            {
                this.Location[1] -= 1;
            }
            else if (dir == 2) // east
            {
                this.Location[0] += 1;
            }
            else if (dir == 3) // west
            {
                this.Location[0] -= 1;
            }
        }

        public void ViewInventory()
        {
            Console.WriteLine("You check your bag.");
            for(int i = 0; i < 3; i++)
            {
                Console.WriteLine($"{i}. {this.Inventory[i].Type}");
            }
        }
    }
}