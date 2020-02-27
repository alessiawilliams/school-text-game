using System;
using TextGame.game.map;
using TextGame.game.ux.util;

namespace TextGame.game {
    public class Player {
        // The player will have a tracked location and an inventory of 3 slots
        public Item[] Inventory = new Item[3];
        public int[] Location = new int[2] {0, 0};
        public int ItemsHeld { get; set; }

        public void Move(int dir) {
            try {
                if (dir == 0) // north
                {
                    this.Location[0] -= 1;
                    SlowPrint.Print("You headed north.");
                }
                else if (dir == 1) // south
                {
                    this.Location[0] += 1;
                    SlowPrint.Print("You ventured south.");
                }
                else if (dir == 2) // east
                {
                    this.Location[1] += 1;
                    SlowPrint.Print("You moved east.");
                }
                else if (dir == 3) // west
                {
                    this.Location[1] -= 1;
                    SlowPrint.Print("You went west.");
                }
            }
            catch {
                Console.WriteLine("You can't see - it's too dangerous to go further that way!");
            }
        }

        public void ViewInventory() {
            // TODO: Fix crashes
            Console.WriteLine("You check your bag.");
            for (int i = 0; i < 3; i++) {
                try {
                    Console.WriteLine($"{i}. {this.Inventory[i].Type}");
                }
                catch { Console.WriteLine($"{i}. Empty"); }
            }

            Console.WriteLine("Press RETURN to return to game.");
            Console.ReadKey();
        }

        public int UseItem(string type) {
            // TODO: Fix crashes
            if (type == "Map") {
                foreach (Item i in this.Inventory) {
                    try {
                        if (i.Type == "Map") {
                            return 1; // Can use map
                        }
                    }
                    catch { }
                }

                return 0; // No map in inventory
            }

            if (type == "Flashlight") {
                foreach (Item i in this.Inventory) {
                    try {
                        if (i.Type == "Flashlight") {
                            return 3; // Can use flashlight
                        }
                    }
                    catch { }
                }

                return 2; // No flashlight in inventory
            }

            return 4; // Neither map nor flashlight in inventory
        }
    }
}