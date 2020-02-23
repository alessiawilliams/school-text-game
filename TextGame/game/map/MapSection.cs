namespace TextGame
{
    public interface MapSection
    {
        // MapSection will be of type Path, Grassland, Generator, Gate, Tree.
        // Each section can contain a random item, like a map, which gives access to the map.
        // this could include a flashlight, allowing the player to see 2 squares in each direction instead of 1.

        Item Item { get; set; }
        bool ContainsPlayer { get; set; }
        string Name { get; set; }

        void Options();
    }
}