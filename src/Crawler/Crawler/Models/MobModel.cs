namespace Crawler.Models
{
    public class MobModel
    {
        public string Name { get; }
        public Graphic Graphic { get; }
        public CharacterStats Stats { get; }

        public MobModel(string name, Graphic graphic, CharacterStats stats)
        {
            Name = name;
            Graphic = graphic;
            Stats = stats;
        }
    }
}