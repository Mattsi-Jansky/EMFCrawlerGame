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

        public void InteractWithEnititiesAt(Entity entity, Point location)
        {
            InteractWithEquippables(entity, location);
        }

        private void InteractWithEquippables(Entity entity, Point location)
        {
            var tile = _map.Get(location);
            var equipables = new List<EquipableComponent>();
            tile.GetEquipables(ref equipables);

            foreach (var equipable in equipables)
            {
                var existingEquippable = entity.GetEquipable(equipable.Slot);
                if (existingEquippable != null)
                {
                    entity.Remove(existingEquippable);
                    entity.RecieveMessage($"You remove the {existingEquippable.GetName()}");
                    _map.Add(new Entity(existingEquippable), location);
                    var droppedItemEntity = (Entity)new Entity(existingEquippable)
                        .Add(new PositionComponent());
                    _moveEntityService.Put(droppedItemEntity, location);
                }

                var equipableEntity = (Entity) equipable.Parent;
                _map.Remove(equipableEntity, location);
                entity.Add(equipable);
                entity.RecieveMessage($"You equip the {equipable.GetName()}");
            }
        }
    }
}