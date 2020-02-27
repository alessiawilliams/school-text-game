using System;
using TextGame.game.map;
using TextGame.game.ux.util;

namespace TextGame.game {
    public class Player {
        public bool FlashActive = false;

        // The player will have a tracked location and an inventory of 3 slots
        public Item[] Inventory = new Item[3];
        public int[] Location = new int[2] {0, 0};
        public int ItemsHeld { get; set; }

        public void Move(int dir) {
            if (dir == 0) {
                if (this.Location[0] != 0) {
                    this.Location[0] -= 1;
                    SlowPrint.Print("You headed north.");
                }
                else {
                    SlowPrint.Print("You can't see - it's too dangerous to go further north!");
                }
            }
            else if (dir == 1) {
                if (this.Location[0] != 9) {
                    this.Location[0] += 1;
                    SlowPrint.Print("You ventured south.");
                }
                else {
                    SlowPrint.Print("You can't see - it's too dangerous to go further south!");
                }
            }
            else if (dir == 2) {
                if (this.Location[1] != 9) {
                    this.Location[1] += 1;
                    SlowPrint.Print("You moved east.");
                }
                else {
                    SlowPrint.Print("You can't see - it's too dangerous to go further east!");
                }
            }
            else if (dir == 3) {
                if (this.Location[1] != 0) {
                    this.Location[1] -= 1;
                    SlowPrint.Print("You moved west.");
                }
                else {
                    SlowPrint.Print("You can't see - it's too dangerous to go further west!");
                }
            }
        }

        public void ViewInventory() {
            // TODO: Fix crashes
            Console.Clear();
            Console.WriteLine("You checked your bag.\n");
            for (int i = 0; i < 3; i++) {
                try {
                    Console.WriteLine($"{i}. {this.Inventory[i].Type}");
                }
                catch {
                    Console.WriteLine($"{i}. Empty");
                }
            }

            Console.WriteLine("\nPress RETURN to return to game.");
            Console.ReadKey();
        }

        public int UseItem(string type) {
            if (type == "Map") {
                for (int i = 0; i < 2; i++) {
                    try {
                        if (this.Inventory[i].Type == "Map") {
                            this.Inventory[i] = null;
                            return 1;
                        }
                    }
                    catch { }
                }

                return 0; // No map in inventory
            }

            if (type == "Flashlight") {
                for (int i = 0; i < 2; i++) {
                    try {
                        if (this.Inventory[i].Type == "Flashlight") {
                            this.Inventory[i] = null;
                            return 3;
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