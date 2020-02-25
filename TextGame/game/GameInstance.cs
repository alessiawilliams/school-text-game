using System;
using TextGame.game.map;
using TextGame.game.map.section_types;

namespace TextGame.game {
    public class GameInstance {
        public GameInstance() {
            // Creates the map and player
            this.Map = new Map();
            this.Player = new Player();

            // Set player's initial location.
            Random r = new Random();
            while (this.Player.Location[0] == 0 && this.Player.Location[1] == 0) {
                int x = r.Next(10);
                int y = r.Next(10);
                if (this.Map.Matrix[x, y] is Grassland) {
                    this.Map.Matrix[x, y].ContainsPlayer = true;
                    this.Player.Location[0] = x;
                    this.Player.Location[1] = y;
                }
            }
        }

        public Player Player { get; set; }
        public Map Map { get; set; }
    }
}