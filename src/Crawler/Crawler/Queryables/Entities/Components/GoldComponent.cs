using System.Collections.Generic;
using Crawler.Models;

namespace Crawler.Queryables.Entities.Components
{
    public class GoldComponent : QueryableAggregator<Component>
    {
        public int Value { get; }

        public GoldComponent(int value)
        {
            Value = value;
            if (value <= 10)
            {
                Add(new GraphicComponent(Graphic.RoundShield_gray));
            }
            else
            {
                Add(new GraphicComponent(Graphic.Gold_pile));
            }
        }

        public override void GetGold(ref IList<GoldComponent> gold)
        {
            gold.Add(this);
        }
    }
}