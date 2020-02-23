using System;
using TextGame.section_types;

namespace TextGame.game
{
    public class GameInstance
    {
        public Player Player { get; set; }
        public Map Map { get; set; }
        
        public GameInstance()
        {
            // Creates the map and player
            this.Map = new Map();
            this.Player =  new Player();
            
            // Set player's initial location.
            Random r = new Random();
            while (Player.Location[0] == 0 && Player.Location[1] == 0)
            {
                int x = r.Next(10);
                int y = r.Next(10);
                if (this.Map.Layout[x, y] is Grassland)
                {
                    this.Map.Layout[x, y].ContainsPlayer = true;
                    Player.Location[0] = x;
                    Player.Location[1] = y;
                }
            }
        }
    }
}