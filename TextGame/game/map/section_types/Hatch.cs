namespace TextGame.section_types
{
    public class Hatch : MapSection
    {
        public bool ContainsPlayer { get; set; }
        public Item Item { get; set; }
        public string Name { get; set; } = "Hatch";

        public void UseHatch()
        {
            
        }

        public void Options()
        {
            
        }
    }
}