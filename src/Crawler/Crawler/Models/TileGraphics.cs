using System;

namespace Crawler.Models
{
    public class TileGraphics
    {
        public Graphic[] Graphics { get; }
        public String Text { get; }

        public TileGraphics(Graphic[] graphics, string text)
        {
            Graphics = graphics;
            Text = text;
        }

        public TileGraphics(Graphic[] graphics)
        {
            Graphics = graphics;
            Text = String.Empty;
        }
    }
}
