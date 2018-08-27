using System.Collections.Generic;
using Crawler.Maps;
using Crawler.Queryables.Entities;
using Crawler.Queryables.Entities.Components;

namespace Crawler.Services
{
    public class InteractionService
    {
        private IMap _map;
        private readonly MoveEntityService _moveEntityService;

        public InteractionService(IMap map, MoveEntityService moveEntityService)
        {
            _map = map;
            _moveEntityService = moveEntityService;
        }

        public void InteractWithEnititiesAtPosition(Entity entity)
        {
            InteractWithEquippables(entity);
        }

        private void InteractWithEquippables(Entity entity)
        {
            var point = entity.GetPosition();
            var tile = _map.Get(point);
            var equipables = new List<EquipableComponent>();
            tile.GetEquipables(ref equipables);

            foreach (var equipable in equipables)
            {
                var existingEquippable = entity.GetEquipable(equipable.Slot);
                if (existingEquippable != null)
                {
                    entity.Remove(existingEquippable);
                    _map.Add(new Entity(existingEquippable), point);
                }

                var equipableEntity = (Entity) equipable.Parent;
                _map.Remove(equipableEntity, point);
                entity.Add(equipable);
            }
        }
    }
}