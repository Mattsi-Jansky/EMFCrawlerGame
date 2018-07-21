using System;
using Crawler.Queryables.Entities;

namespace Crawler.Maps.EntityPlacers
{
    public class RandomEntityPlacer :IEntityPlacer
    {
        Random random = new Random();

        public Point PlaceCharacter(IMap map, Entity entity)
        {
            var position = new Point(random.Next(map.Size.X - 1), random.Next(map.Size.Y - 1));
            map.Add(entity, position);
            return position;
        }
    }
}
