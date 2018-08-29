using Crawler.Models;

namespace Crawler.Queryables.Entities.Components
{
    public class CharacterComponent : Component
    {
        private int str, dex, con, wis;
        private int hp;
        private int baseAc;
        private static WeaponComponent fisticuffs = new WeaponComponent(new Dice(1,4), Attribute.Str, null, "Fisticuffs", Graphic.Apple); 

        public CharacterComponent(int str, int dex, int con, int wis)
        {
            this.str = 8 + str;
            this.dex = 8 + dex;
            this.con = 8 + con;
            this.wis = 8 + wis;
            hp = con;
            baseAc = 10 + CharacterStats.GetAbilityModifier(dex);
        }

        public CharacterComponent(CharacterStats stats) : this(stats.str, stats.dex, stats.con, stats.wis) { }

        public override void GetStrength(ref int value)
        {
            value += str;
        }

        public override void GetDexterity(ref int value)
        {
            value += dex;
        }

        public override void GetConstitution(ref int value)
        {
            value += con;
        }

        public override void GetWisdom(ref int value)
        {
            value += wis;
        }
        
        public override void GetWeapon(ref WeaponComponent weapon)
        {
            //If no weapon equipped default to low damage for fists
            if (weapon == null)
            {
                weapon = fisticuffs;
            }
        }

        public override void TakeDamage(int damage)
        {
            hp -= damage;
        }

        public override bool IsDead()
        {
            return hp <= 0;
        }
    }
}