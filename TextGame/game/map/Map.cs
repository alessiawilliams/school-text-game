using System;
using TextGame.section_types;

namespace TextGame
{
    public class Map
    {
        // A map will be created which is comprised of many objects implementing MapSection.
        // A map is essentially a 10x10 matrix of objects.

        public readonly MapSection[,] Layout;

        // When a new Map is instantiated, It will create 6 generator objects, 1 hatch object, 13 tree objects, and 80 grassland objects
        public Map()
        {
            this.Layout = new MapSection[10, 10];
            Random r = new Random();

            // Create hatch
            this.Layout[r.Next(10), r.Next(10)] = new Hatch();
            
            // Create generators
            for (int i = 0; i < 6; i++)
            {
                int x = r.Next(10);
                int y = r.Next(10);
                if (this.Layout[x, y] is MapSection)
                {
                    i--;
                }
                else
                {
                    this.Layout[x, y] = new Generator();
                }
            }
            
            // Create trees
            for (int i = 0; i < 13; i++)
            {
                int x = r.Next(10);
                int y = r.Next(10);
                if (this.Layout[x, y] is MapSection)
                {
                    i--;
                }
                else
                {
                    this.Layout[x, y] = new Tree();
                }
            }

            // Add 80 Grassland objects
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (this.Layout[i, j] == null) { this.Layout[i, j] = new Grassland(); }
                }
            }
        }
    }
}