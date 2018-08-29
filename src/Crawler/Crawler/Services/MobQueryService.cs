using Crawler.Maps;
using Crawler.Queryables.Entities;
using Crawler.Queryables.Entities.Components;
using Crawler.Queryables.Tiles;

namespace Crawler.Services
{
    public class MobQueryService
    {
        private IMap _map;
        private Entity entity;

        public MobQueryService(IMap map, Entity entity)
        {
            _map = map;
            this.entity = entity;
        }

        public Point? GetNearbyOpponentDirection()
        {
            var location = entity.GetPosition();
            var tile = _map.Get(location);
            var east = _map.Get(location.East());
            var west = _map.Get(location.West());
            var north = _map.Get(location.North());
            var south = _map.Get(location.South());

            if (HasOpponent(east)) return new Point(1,0);
            if (HasOpponent(west)) return new Point(-1,0);
            if (HasOpponent(north)) return new Point(0, -1);
            if (HasOpponent(south)) return new Point(0, 1);
            return null;
        }

        private bool HasOpponent(Tile tile)
        {
            WeaponComponent weapon = null;
            tile.GetWeapon(ref weapon);
            return weapon != null;
        }
    }
}