using System;
using TextGame.game.map;

namespace TextGame.game {
    public class Player {
        public Item[] Inventory = new Item[3];

        // The player will have a tracked location and an inventory of 3 slots
        public int[] Location = new int[2] {0, 0};
        public int ItemsHeld { get; set; }

        public void Move(int dir) {
            if (dir == 0) // north
            {
                this.Location[1] += 1;
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

        public void ViewInventory() {
            Console.WriteLine("You check your bag.");
            for (int i = 0; i < 3; i++) {
                Console.WriteLine($"{i}. {this.Inventory[i].Type}");
            }
        }

        public int UseItem(string type) {
            if (type == "Map") {
                foreach (Item i in this.Inventory) {
                    if (i.Type == "Map") {
                        return 1; // Can use map
                    }
                }

                return 0; // No map in inventory
            }

            if (type == "Flashlight") {
                foreach (Item i in this.Inventory) {
                    if (i.Type == "Flashlight") {
                        return 3; // Can use flashlight
                    }
                }

                return 2; // No flashlight in inventory
            }

            return 4; // Neither map nor flashlight in inventory
        }
    }
}