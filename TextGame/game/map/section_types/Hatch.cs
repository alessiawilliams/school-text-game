namespace TextGame.game.map.section_types {
    public class Hatch : IMapSection {
        public bool ContainsPlayer { get; set; }
        public Item Item { get; set; }
        public string Name { get; set; } = "Hatch";

        public void Options() { }

        public void UseHatch() { }
    }
}