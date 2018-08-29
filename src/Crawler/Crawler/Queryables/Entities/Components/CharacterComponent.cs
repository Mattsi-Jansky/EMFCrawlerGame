using Crawler.Factories;
using Crawler.Models;

namespace Crawler.Queryables.Entities.Components
{
    public class CharacterComponent : Component
    {
        private int _str, _dex, _con, _wis;
        private int hp;
        private int baseAc;
        private WeaponComponent fisticuffs; 

        public CharacterComponent(int str, int dex, int con, int wis)
        {
            _str = 8 + str;
            _dex = 8 + dex;
            _con = 8 + con;
            _wis = 8 + wis;
            hp = _con * 3;
            baseAc = 10 + CharacterStats.GetAbilityModifier(_dex);
            fisticuffs = new WeaponFactory().Get(Weapon.Fisticuffs);
        }

        public CharacterComponent(CharacterStats stats) : this(stats.str, stats.dex, stats.con, stats.wis) { }

        public override void GetStrength(ref int value)
        {
            value += _str;
        }

        public override void GetDexterity(ref int value)
        {
            value += _dex;
        }

        public override void GetConstitution(ref int value)
        {
            value += _con;
        }

        public override void GetWisdom(ref int value)
        {
            value += _wis;
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