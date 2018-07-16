using System.Collections.Generic;
using Crawler.Models;

namespace Crawler.Queryables.Entities.Components
{
    public class GraphicComponent : Component
    {
        private Graphic graphic;

        public GraphicComponent(Graphic graphic)
        {
            this.graphic = graphic;
        }

        public override void GetGraphics(ref IList<Graphic> graphics)
        {
            graphics.Add(graphic);
        }
    }
}