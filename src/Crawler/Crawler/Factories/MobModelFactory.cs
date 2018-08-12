using System.Collections.Generic;
using Crawler.Models;

namespace Crawler.Factories
{
    public static class MobModelFactory
    {
        private static readonly MobModel SkeletonWarrior = new MobModel("Skeleton Warrior", Graphic.SkeletonWarrior);
        private static readonly MobModel SkeletonArcher = new MobModel("Skeleton Archer", Graphic.SkeletonRanger);
        private static readonly MobModel SkeletonRogue = new MobModel("Skeleton Rogue", Graphic.SkeletonRogue);
        private static readonly MobModel SkeletonKnight = new MobModel("Skeleton Knight", Graphic.SkeletonHellKnight);
        private static readonly MobModel SkeletonWarlock = new MobModel("Skeleton Warlock", Graphic.SkeletonWarlock);
        private static readonly MobModel Spider = new MobModel("Sneaky Spooder", Graphic.Spider);
        private static readonly MobModel SpiderRed = new MobModel("Deadly Spooder", Graphic.SpiderRed);
        private static readonly MobModel SpiderGreen = new MobModel("Toxic Spooder?", Graphic.SpiderGreen);
        private static readonly MobModel CatBlack = new MobModel("Kitty", Graphic.CatBlack);
        private static readonly MobModel SnakePurple = new MobModel("Snake", Graphic.SnakePurple);
        private static readonly MobModel SnakeGreen = new MobModel("Snake", Graphic.SnakeGreen);
        private static readonly MobModel ScorpionPale = new MobModel("Scorpion", Graphic.ScorpionPale);
        private static readonly MobModel ScorpionBlack = new MobModel("Scorpion", Graphic.ScorpionBlack);
        private static readonly MobModel Bat = new MobModel("Bat", Graphic.Bat);
        private static readonly MobModel BatDark = new MobModel("Evil Bat", Graphic.BatDark);
        private static readonly MobModel GoblinWarrior = new MobModel("Goblin", Graphic.GoblinWarrior);
        private static readonly MobModel GoblingArmouredWarrior = new MobModel("Goblin", Graphic.GoblingArmouredWarrior);
        private static readonly MobModel GoblinArmouredShieldedWarrior = new MobModel("Goblin", Graphic.GoblinArmouredShieldedWarrior);
        private static readonly MobModel GoblinMage = new MobModel("Goblin Magicker", Graphic.GoblinMage);
        
        public static readonly IList<IList<MobModel>> MobThemes = new List<IList<MobModel>>
        {
            new List<MobModel> { SkeletonArcher, SkeletonWarrior, SkeletonRogue, SkeletonKnight, SkeletonWarlock},
            new List<MobModel> {Spider, SpiderRed, SpiderGreen, CatBlack, SnakePurple, SnakeGreen, ScorpionPale, ScorpionBlack, Bat, BatDark},
            new List<MobModel> {GoblinWarrior, GoblinArmouredShieldedWarrior, GoblingArmouredWarrior, GoblinMage},
            
        };
    }
}