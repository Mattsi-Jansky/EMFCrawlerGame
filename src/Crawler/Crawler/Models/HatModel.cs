namespace Crawler.Models
{
    public class HatModel
    {
        public string Name { get; set; }
        public Graphic Graphic { get; set; }
        public string PreTitle { get; set; }
        public string PostTitle { get; set; }

        public HatModel(string name, Graphic graphic, string preTitle, string postTitle)
        {
            Name = name;
            Graphic = graphic;
            PostTitle = postTitle;
            PreTitle = preTitle;
        }
    }
}