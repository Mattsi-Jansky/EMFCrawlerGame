using System.Collections.Generic;
using Crawler.Models;

namespace Crawler.Queryables.Entities.Components
{
    public class WeaponComponent : EquipableComponent
    {
        public readonly Dice DamageDice;
        private readonly Attribute _toHitAttribute;
        private readonly Attribute? _damageBonus;
        public readonly string Name;
        public readonly Graphic Graphic;

        public WeaponComponent(Dice damageDice, Attribute toHitAttribute, Attribute? damageBonus, string name, Graphic graphic) : base(EquipableSlot.Weapon)
        {
            DamageDice = damageDice;
            _toHitAttribute = toHitAttribute;
            _damageBonus = damageBonus;
            Name = name;
            Graphic = graphic;
        }

        public override void GetHitBonus(ref int value)
        {
            if (_toHitAttribute == Attribute.Str)
            {
                int str = 0;
                Parent.GetStrength(ref str);
                value += CharacterStats. GetAbilityModifier(str);
            }
            else if (_toHitAttribute == Attribute.Dex)
            {
                int dex = 0;
                Parent.GetDexterity(ref dex);
                value += CharacterStats.GetAbilityModifier(dex);
            }
            else if (_toHitAttribute == Attribute.Wis)
            {
                int wis = 0;
                Parent.GetWisdom(ref wis);
                value += CharacterStats.GetAbilityModifier(wis);
            }
        }

        public override void GetWeapon(ref WeaponComponent weapon)
        {
            if (weapon == null || DamageDice.Greater(weapon.DamageDice))
            {
                weapon = this;
            }
        }

        public override void GetDamageBonus(ref int value)
        {
            if (_damageBonus.HasValue)
            {
                if (_damageBonus.Value == Attribute.Str)
                {
                    int str = 0;
                    Parent.GetStrength(ref str);
                    value += CharacterStats.GetAbilityModifier(str);
                }
                else if (_damageBonus.Value == Attribute.Dex)
                {
                    int dex = 0;
                    Parent.GetDexterity(ref dex);
                    value += CharacterStats.GetAbilityModifier(dex);
                }
                else if (_damageBonus.Value == Attribute.Wis)
                {
                    int wis = 0;
                    Parent.GetWisdom(ref wis);
                    value += CharacterStats.GetAbilityModifier(wis);
                }
            }
        }

        public Entity AsEntity()
        {
            var entity = new Entity(this);
            entity.Add(new GraphicComponent(Graphic));
            entity.Add(new NameComponent(Name));
            return entity;
        }
    }
}