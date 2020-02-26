using System;
using TextGame.game.map.section_types;

namespace TextGame.game.map {
    public class Map {
        // A map will be created which is comprised of many objects implementing IMapSection.
        // A map is essentially a 10x10 matrix of objects.

        public readonly IMapSection[,] Matrix;
        public int ActiveGenerators { get; set; }

        // When a new Map is instantiated, It will create 6 generator objects, 1 hatch object, 13 tree objects, and 80 grassland objects
        public Map() {
            this.Matrix = new IMapSection[10, 10];
            Random r = new Random();

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
        }
    }
}