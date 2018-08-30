using System.Collections.Generic;
using Crawler.Models;

namespace Crawler.Factories
{
    public static class MobModelFactory
    {
        private static readonly MobModel SkeletonWarrior = new MobModel("Skeleton Warrior", Graphic.SkeletonWarrior, new CharacterStats(8, 12, 5, 6), Weapon.ShortSword);
        private static readonly MobModel SkeletonArcher = new MobModel("Skeleton Archer", Graphic.SkeletonRanger, new CharacterStats(8, 12, 5, 6), Weapon.Shortbow);
        private static readonly MobModel SkeletonRogue = new MobModel("Skeleton Rogue", Graphic.SkeletonRogue, new CharacterStats(8, 12, 4, 6), Weapon.ShortSword);
        private static readonly MobModel SkeletonKnight = new MobModel("Skeleton Knight", Graphic.SkeletonHellKnight, new CharacterStats(8, 10, 10, 6), Weapon.GreatSword);
        private static readonly MobModel SkeletonWarlock = new MobModel("Skeleton Warlock", Graphic.SkeletonWarlock, new CharacterStats(8, 8, 4, 10), Weapon.StoneStave);
        private static readonly MobModel Spider = new MobModel("Sneaky Spooder", Graphic.Spider, new CharacterStats(10, 13, 9, 10), Weapon.Teeth);
        private static readonly MobModel SpiderRed = new MobModel("Deadly Spooder", Graphic.SpiderRed, new CharacterStats(11, 13, 9, 10), Weapon.Teeth);
        private static readonly MobModel SpiderGreen = new MobModel("Toxic Spooder?", Graphic.SpiderGreen, new CharacterStats(10, 13, 9, 10), Weapon.Teeth);
        private static readonly MobModel CatBlack = new MobModel("Kitty", Graphic.CatBlack, new CharacterStats(9, 12, 9, 8), Weapon.Claws);
        private static readonly MobModel SnakePurple = new MobModel("Snake", Graphic.SnakePurple, new CharacterStats(7, 13, 7, 7), Weapon.Claws);
        private static readonly MobModel SnakeGreen = new MobModel("Snake", Graphic.SnakeGreen, new CharacterStats(7, 13, 7, 7), Weapon.Claws);
        private static readonly MobModel ScorpionPale = new MobModel("Scorpion", Graphic.ScorpionPale, new CharacterStats(7, 13, 6, 5), Weapon.Tail);
        private static readonly MobModel ScorpionBlack = new MobModel("Scorpion", Graphic.ScorpionBlack, new CharacterStats(7, 13, 6, 5), Weapon.Tail);
        private static readonly MobModel Bat = new MobModel("Bat", Graphic.Bat, new CharacterStats(10, 11, 7, 8), Weapon.Teeth);
        private static readonly MobModel BatDark = new MobModel("Evil Bat", Graphic.BatDark, new CharacterStats(10, 11, 8, 9), Weapon.Teeth);
        private static readonly MobModel GoblinWarrior = new MobModel("Goblin", Graphic.GoblinWarrior, new CharacterStats(9, 12, 8, 8), Weapon.ShortSword);
        private static readonly MobModel GoblingArmouredWarrior = new MobModel("Goblin", Graphic.GoblingArmouredWarrior, new CharacterStats(9, 11, 10, 8),  Weapon.LongSword);
        private static readonly MobModel GoblinArmouredShieldedWarrior = new MobModel("Goblin", Graphic.GoblinArmouredShieldedWarrior, new CharacterStats(9, 10, 12, 8), Weapon.GreatSword);
        private static readonly MobModel GoblinMage = new MobModel("Goblin Magicker", Graphic.GoblinMage, new CharacterStats(8, 11, 7, 12), Weapon.GemStave);
        private static readonly MobModel Troll = new MobModel("Troll", Graphic.Troll, new CharacterStats(11, 9, 11, 9), Weapon.ShortSword);
        private static readonly MobModel TrollWarrior = new MobModel("Troll", Graphic.TrollWarrior, new CharacterStats(11, 9, 1, 9), Weapon.LongSword);
        private static readonly MobModel OgreWarrior = new MobModel("Ogre", Graphic.OgreWarrior, new CharacterStats(11, 8, 13, 7), Weapon.LongSword);
        private static readonly MobModel OgreWarriorGray = new MobModel("Ogre", Graphic.OgreWarriorGray, new CharacterStats(11, 8, 13, 7), Weapon.GreatSword);
        private static readonly MobModel OgreWarriorArmoured = new MobModel("Ogre", Graphic.OgreWarriorArmoured, new CharacterStats(11, 7, 14, 7), Weapon.GreatSword);
        private static readonly MobModel OgreWarriorArmouredGray = new MobModel("Ogre", Graphic.OgreWarriorArmouredGray, new CharacterStats(11, 7, 15, 7), Weapon.GreatSword);
        private static readonly MobModel GiantWarrior = new MobModel("Giant", Graphic.GiantWarrior, new CharacterStats(12, 8, 12, 8), Weapon.ShortSword);
        private static readonly MobModel GiantBarbarianMale = new MobModel("Giant", Graphic.GiantBarbarianMale, new CharacterStats(12, 8, 12, 8), Weapon.ShortSword);
        private static readonly MobModel GiantBarbarianFemale = new MobModel("Giant", Graphic.GiantBarbarianFemale, new CharacterStats(12, 8, 12, 8), Weapon.ShortSword);
        private static readonly MobModel GiantMonk = new MobModel("Giant", Graphic.GiantMonk, new CharacterStats(12, 10, 11, 8), Weapon.Fisticuffs);
        private static readonly MobModel RedTroll = new MobModel("Troll", Graphic.RedTroll, new CharacterStats(12, 12, 12, 10), Weapon.LongSword);
        private static readonly MobModel WaterElemental = new MobModel("Water Elemental", Graphic.ElementalWater, new CharacterStats(10, 13, 8, 13), Weapon.ElementalBreath);
        private static readonly MobModel IceElemental = new MobModel("Ice Elemental", Graphic.ElementalIce, new CharacterStats(10, 13, 8, 13), Weapon.ElementalBreath);
        private static readonly MobModel WaterColossus = new MobModel("Water Colossus", Graphic.WaterColossus, new CharacterStats(13, 12, 10, 13), Weapon.ColossalFist);
        private static readonly MobModel IceColossus = new MobModel("Ice Colossus", Graphic.IceColossus, new CharacterStats(13, 9, 14, 13), Weapon.ColossalFist);
        private static readonly MobModel IceSpectre = new MobModel("Ice Spectre", Graphic.IceSpectre, new CharacterStats(8, 14, 12, 11), Weapon.ElementalBreath);
        private static readonly MobModel IceSkull = new MobModel("Ice Skull", Graphic.IceSkull, new CharacterStats(8, 15, 9, 14), Weapon.ElementalBreath);
        private static readonly MobModel FireSkull = new MobModel("Fire Skull", Graphic.FlameSkull, new CharacterStats(8, 14, 9, 15), Weapon.ElementalBreath);
        private static readonly MobModel FireElemental = new MobModel("Fire Elemental", Graphic.ElementalFire, new CharacterStats(10, 12, 8, 14), Weapon.ElementalBreath);
        private static readonly MobModel FireColossus = new MobModel("Fire Colossus", Graphic.FireColossus, new CharacterStats(14, 10, 10, 13), Weapon.ColossalFist);
        
        public static readonly IList<IList<MobModel>> MobThemes = new List<IList<MobModel>>
        {
            new List<MobModel> { SkeletonArcher, SkeletonWarrior, SkeletonRogue, SkeletonKnight, SkeletonWarlock},
            new List<MobModel> {Spider, SpiderRed, SpiderGreen, CatBlack, SnakePurple, SnakeGreen, ScorpionPale, ScorpionBlack, Bat, BatDark},
            new List<MobModel> {GoblinWarrior, GoblinArmouredShieldedWarrior, GoblingArmouredWarrior, GoblinMage},
            new List<MobModel> {Troll, TrollWarrior, RedTroll},
            new List<MobModel> {OgreWarrior, OgreWarriorGray, OgreWarriorArmoured, OgreWarriorArmouredGray},
            new List<MobModel> {GiantWarrior, GiantBarbarianFemale, GiantBarbarianMale, GiantMonk},
        };

        public static readonly IList<MobModel> WaterThemedMobs = new List<MobModel>
        {
            WaterElemental, IceElemental, WaterColossus, IceColossus, IceSpectre, IceSkull
        };

        public static readonly IList<MobModel> FireThemedMobs = new List<MobModel>
        {
            FireSkull, FireElemental, FireColossus
        };
    }
}