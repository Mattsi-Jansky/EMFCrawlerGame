using System.Collections.Generic;
using Crawler.Models;

namespace Crawler.Queryables.Entities.Components
{
    public class WeaponComponent : EquipableComponent
    {
        private Dice _damageDice;
        private Attribute _toHitAttribute;
        private Attribute? _damageBonus;

        public WeaponComponent(Dice damageDice, Attribute toHitAttribute, Attribute? damageBonus) : base(EquipableSlot.Weapon)
        {
            _damageDice = damageDice;
            _toHitAttribute = toHitAttribute;
            _damageBonus = damageBonus;
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

        public override void GetDamageDice(ref Dice dice)
        {
            if (dice != null && _damageDice.Greater(dice))
            {
                dice = _damageDice;
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
    }
}