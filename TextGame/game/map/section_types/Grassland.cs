namespace TextGame.game.map.section_types {
    public class Grassland : IMapSection {
        public bool ContainsPlayer { get; set; }
        public Item Item { get; set; }
        public string Name { get; set; } = "Grassland";

        // Grassland has no options; this method stub only exists as Grassland implements MapSection.
        public void Options(int gensActive) { }
    }
}