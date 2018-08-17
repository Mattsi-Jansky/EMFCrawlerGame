using System;
using Crawler.Queryables.Entities;

namespace Crawler.Maps.EntityPlacers
{
    public class RandomEntityPlacer : IEntityPlacer
    {
        private readonly Random _random = new Random();

        public Point PlaceEntity(IMap map, Entity entity)
        {
            var position = new Point(_random.Next(map.Size.X - 1), _random.Next(map.Size.Y - 1));
            if (!map.Get(position).IsBlocked()) return position;
            else return PlaceEntity(map, entity);
        }
    }
}
