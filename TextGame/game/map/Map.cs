using System;
using TextGame.game.map.section_types;

namespace TextGame.game.map {
    public class Map {
        // A map will be created which is comprised of many objects implementing IMapSection.
        // A map is essentially a 10x10 matrix of objects.

        public readonly IMapSection[,] Matrix;

        // When a new Map is instantiated, It will create 6 generator objects, 1 hatch object, 13 tree objects, and 80 grassland objects
        public Map() {
            this.Matrix = new IMapSection[10, 10];
            Random r = new Random();
            int unluckyGenerators;

            // Create hatch
            this.Matrix[r.Next(10), r.Next(10)] = new Hatch();

            // Create generators
            for (int i = 0; i < 6; i++) {
                int x = r.Next(10);
                int y = r.Next(10);
                if (this.Matrix[x, y] is IMapSection) {
                    i--;
                }
                else {
                    this.Matrix[x, y] = new Generator();
                    if (unluckyGenerators < 2) {
                        if(r.Next(6) <= 2) {
                            this.Matrix[x, y].Unlucky = true;
                        }
                    }
                }
            }

            // Create trees
            for (int i = 0; i < 13; i++) {
                int x = r.Next(10);
                int y = r.Next(10);
                if (this.Matrix[x, y] is IMapSection) {
                    i--;
                }
                else {
                    this.Matrix[x, y] = new Tree();
                }
            }

            // Add 80 Grassland objects
            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    if (this.Matrix[i, j] == null) {
                        this.Matrix[i, j] = new Grassland();
                    }
                }
            }

            // Makes sure 2 unlucky generators were made. If not, up to the first 2 found are made unlucky.
            // X ~ B(6, 1/3) : P(X <= 1) == 35.11% chance this has to run.
            while (unluckyGenerators < 2) {
                for (int i = 0; i < 10; i++) {
                    for (int j = 0; j < 10; j++) {
                        if (unluckyGenerators < 2 && this.Matrix[i, j] is Generator) {
                            unluckyGenerators++;
                            this.Matrix[i, j].Unlucky = true;
                        }
                    }
                }
            }
        }

        public int ActiveGenerators { get; set; }
    }
}