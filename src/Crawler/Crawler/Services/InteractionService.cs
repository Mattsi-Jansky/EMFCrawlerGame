using System;
using System.Collections.Generic;
using System.Linq;
using Crawler.Maps;
using Crawler.Models;
using Crawler.Queryables.Entities;
using Crawler.Queryables.Entities.Components;
using Crawler.Queryables.Tiles;

namespace Crawler.Services
{
    public class InteractionService
    {
        private IMap _map;
        private readonly MoveEntityService _moveEntityService;
        private readonly Random _random;
        private readonly CombatService _combatService;

        public InteractionService(IMap map, MoveEntityService moveEntityService, Random random, CombatService combatService)
        {
            _map = map;
            _moveEntityService = moveEntityService;
            _random = random;
            _combatService = combatService;
        }

        public void InteractWithEnititiesAt(Entity entity, Point location)
        {
            InteractWithEquippables(entity, location);
            InteractWithGold(entity, location);
            if (_map.Get(location).IsBlocked())
            {
                InteractWithBlockingObject(entity, location);
            }
        }

        private void InteractWithBlockingObject(Entity entity, Point location)
        {
            _combatService.Attack(entity, location);
        }

        private void InteractWithGold(Entity entity, Point location)
        {
            IList<GoldComponent> goldOnTile = new List<GoldComponent>();
            _map.Get(location).GetGold(ref goldOnTile);

            foreach (var gold in goldOnTile)
            {
                entity.GiveGold(gold.Value);
                entity.RecieveMessage($"You find {gold.Value} gold!");
                _map.Remove((Entity)gold.Parent, location);
            }
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