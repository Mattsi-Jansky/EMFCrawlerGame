namespace Crawler.Models
{
    public class MobModel
    {
        public string Name { get; }
        public Graphic Graphic { get; }
        public CharacterStats Stats { get; }
        public Weapon Weapon { get; }

        public MobModel(string name, Graphic graphic, CharacterStats stats, Weapon weapon = Weapon.Fisticuffs)
        {
            Name = name;
            Graphic = graphic;
            Stats = stats;
            Weapon = weapon;
        }
    }
}