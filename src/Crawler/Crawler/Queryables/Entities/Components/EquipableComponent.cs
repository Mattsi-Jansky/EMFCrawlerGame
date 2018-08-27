using System.Collections.Generic;
using Crawler.Models;

namespace Crawler.Queryables.Entities.Components
{
    public class EquipableComponent : QueryableAggregator<Component>
    {
        public EquipableSlot Slot { get; }

        public EquipableComponent(EquipableSlot slot)
        {
            Slot = slot;
        }

        public override EquipableComponent GetEquipable(EquipableSlot slot)
        {
            return Slot == slot ? this : null;
        }

        public override void GetEquipables(ref List<EquipableComponent> equipables)
        {
            equipables.Add(this);
        }

        /*
        public override void Interact(Entity entity)
        {
            var existingEquippable = entity.GetEquipable(_slot);

            if (existingEquippable != null)
            {
                existingEquippable.DetatchParent();
                var tile = (QueryableAggregator<Entity>) entity.Parent;
                tile.Add(existingEquippable);
            }
            
            Parent.DetatchParent();
            entity.Add(this);
        }*/
    }
}