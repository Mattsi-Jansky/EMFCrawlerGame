using System;
using Crawler.Maps;
using Crawler.Queryables.Entities;

namespace Crawler.Services
{
    public class MoveEntityService : PutEntityService
    {
        public MoveEntityService(IMap map) : base(map)
        {
        }

        public void Move(Entity entity, Point targetPosition)
        {
            var fromPosition = new Point?();
            entity.GetPosition(ref fromPosition);

            if(!fromPosition.HasValue) throw new InvalidOperationException("Entity must have a position to move");

            Map.Remove(entity, fromPosition.Value);
            Put(entity, targetPosition);
        }

        public bool IsLegalTileToOccupy(Point targetPosition)
        {
            return targetPosition.X > 0 && targetPosition.Y > 0
                                        && targetPosition.X < Map.Size.X
                                        && targetPosition.Y < Map.Size.Y
                                        && !Map.Get(targetPosition).IsBlocked();
        }
    }
}
