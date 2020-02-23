namespace TextGame.section_types
{
    public class Grassland : MapSection
    {
        public bool ContainsPlayer { get; set; }
        public Item Item { get; set; }
        public string Name { get; set; } = "Grassland";

        public void DropItem()
        {
            
        }

        public void Options()
        {
            // Grassland has no options; this method stub only exists as Grassland implements MapSection.
        }
    }
}