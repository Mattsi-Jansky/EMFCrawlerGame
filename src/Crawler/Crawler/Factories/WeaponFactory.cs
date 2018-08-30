using System;
using System.Collections.Generic;
using Crawler.Models;
using Crawler.Queryables.Entities.Characters;
using Crawler.Queryables.Entities.Components;
using Attribute = Crawler.Models.Attribute;

namespace Crawler.Factories
{
    public class WeaponFactory
    {
        private Dictionary<Archetype, Weapon> weaponsForArchetypes = new Dictionary<Archetype, Weapon>
        {
            {Archetype.Warlock, Weapon.GemStave},
            {Archetype.HellKnight, Weapon.LongSword},
            {Archetype.Druid, Weapon.GemStave},
            {Archetype.HighMage, Weapon.GemStave},
            {Archetype.Mage, Weapon.ShortSword},
            {Archetype.Mindcrafter, Weapon.GemStave},
            {Archetype.Mystic, Weapon.GemStave},
            {Archetype.Paladin, Weapon.LongSword},
            {Archetype.Priest, Weapon.ShortSword},
            {Archetype.Ranger, Weapon.Longbow},
            {Archetype.Rogue, Weapon.ShortSword},
            {Archetype.Warrior, Weapon.LongSword},
            {Archetype.WarriorMage, Weapon.ShortSword},
        };
        
        private Dictionary<Weapon, Func<WeaponComponent>> weaponGenerators =
            new Dictionary<Weapon, Func<WeaponComponent>>
            {
                {Weapon.Fisticuffs, () => new WeaponComponent(new Dice(1,4), Attribute.Str, null, "Fisticuffs", Graphic.Apple)},
                {Weapon.ShortSword, () => new WeaponComponent(new Dice(1,6), Attribute.Dex, Attribute.Str, "Short Sword", Graphic.Sword)},
                {Weapon.LongSword, () => new WeaponComponent(new Dice(1,8), Attribute.Str, Attribute.Str, "Long Sword", Graphic.Sword_goldhilt)},
                {Weapon.GreatSword, () => new WeaponComponent(new Dice(2,6), Attribute.Str, Attribute.Str, "Great Sword", Graphic.Sword_goldfull)},
                {Weapon.MagicSword, () => new WeaponComponent(new Dice(2,6), Attribute.Str, Attribute.Str, "Great Sword", Graphic.Sword_magic)},
                {Weapon.Shortbow, () => new WeaponComponent(new Dice(1,6), Attribute.Dex, Attribute.Dex, "Short Bow", Graphic.Bow_brown)},
                {Weapon.Longbow, () => new WeaponComponent(new Dice(1,8), Attribute.Dex, Attribute.Dex, "Long Bow", Graphic.Bow_iron)},
                {Weapon.ElvenBow, () => new WeaponComponent(new Dice(2,6), Attribute.Dex, Attribute.Dex, "Elven Bow", Graphic.Bow_gold)},
                {Weapon.MagicBow, () => new WeaponComponent(new Dice(2,6), Attribute.Dex, Attribute.Dex, "Magic Bow", Graphic.Bow_magic)},
                {Weapon.StoneStave, () => new WeaponComponent(new Dice(2,6), Attribute.Wis, Attribute.Wis, "Stone Stave", Graphic.Wand_plain)},
                {Weapon.GemStave, () => new WeaponComponent(new Dice(2,6), Attribute.Wis, Attribute.Wis, "Gem Stave", Graphic.Wand_blue)},
                {Weapon.SapphireStave, () => new WeaponComponent(new Dice(2,6), Attribute.Wis, Attribute.Wis, "Sapphire Stave", Graphic.Wand_red)},
                {Weapon.DiamondStave, () => new WeaponComponent(new Dice(2,6), Attribute.Wis, Attribute.Wis, "Diamond Stave", Graphic.Wand_green)},
                {Weapon.Claws, () => new WeaponComponent(new Dice(1,6), Attribute.Dex, null, "Claws", Graphic.CatBlack)},
                {Weapon.Teeth, () => new WeaponComponent(new Dice(1,6), Attribute.Wis, Attribute.Str, "Teeth", Graphic.DemonDogRed)},
                {Weapon.Tail, () => new WeaponComponent(new Dice(1,4), Attribute.Dex, null, "Tail", Graphic.ScorpionBlack)},
                {Weapon.ColossalFist, () => new WeaponComponent(new Dice(2,6), Attribute.Str, Attribute.Str, "Colossal Fist", Graphic.Apple)},
                {Weapon.ElementalBreath, () => new WeaponComponent(new Dice(2,6), Attribute.Dex, Attribute.Wis, "Elemental Breath", Graphic.Wand_red)},
            };

        public WeaponComponent Get(Weapon weapon)
        {
            return weaponGenerators[weapon]();
        }

        public WeaponComponent Get(Archetype archetype)
        {
            var weaponType = weaponsForArchetypes[archetype];
            return Get(weaponType);
        }
    }
}