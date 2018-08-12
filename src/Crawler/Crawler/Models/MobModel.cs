namespace Crawler.Models
{
    public class MobModel
    {
        public string Name { get; }
        public Graphic Graphic { get; }

        public MobModel(string name, Graphic graphic)
        {
            Name = name;
            Graphic = graphic;
        }
    }
}