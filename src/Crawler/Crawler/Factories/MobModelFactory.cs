using System.Collections.Generic;
using Crawler.Models;

namespace Crawler.Factories
{
    public static class MobModelFactory
    {
        private static MobModel SkeletonWarrior = new MobModel("Skeleton Warrior", Graphic.SkeletonWarrior);
        private static MobModel SkeletonArcher = new MobModel("Skeleton Archer", Graphic.SkeletonRanger);
        private static MobModel SkeletonRogue = new MobModel("Skeleton Rogue", Graphic.SkeletonRogue);
        private static MobModel SkeletonKnight = new MobModel("Skeleton Knight", Graphic.SkeletonHellKnight);
        private static MobModel SkeletonWarlock = new MobModel("Skeleton Warlock", Graphic.SkeletonWarlock);
        private static MobModel Spider = new MobModel("Sneaky Spooder", Graphic.Spider);
        private static MobModel SpiderRed = new MobModel("Deadly Spooder", Graphic.SpiderRed);
        private static MobModel SpiderGreen = new MobModel("Toxic Spooder?", Graphic.SpiderGreen);
        private static MobModel CatBlack = new MobModel("Kitty", Graphic.CatBlack);
        private static MobModel SnakePurple = new MobModel("Snake", Graphic.SnakePurple);
        private static MobModel SnakeGreen = new MobModel("Snake", Graphic.SnakeGreen);
        private static MobModel ScorpionPale = new MobModel("Scorpion", Graphic.ScorpionPale);
        private static MobModel ScorpionBlack = new MobModel("Scorpion", Graphic.ScorpionBlack);
        private static MobModel Bat = new MobModel("Bat", Graphic.Bat);
        private static MobModel BatDark = new MobModel("Evil Bat", Graphic.BatDark);
        private static MobModel GoblinWarrior = new MobModel("Goblin", Graphic.GoblinWarrior);
        private static MobModel GoblingArmouredWarrior = new MobModel("Goblin", Graphic.GoblingArmouredWarrior);
        private static MobModel GoblinArmouredShieldedWarrior = new MobModel("Goblin", Graphic.GoblinArmouredShieldedWarrior);
        private static MobModel GoblinMage = new MobModel("Goblin Magicker", Graphic.GoblinMage);
        
        
        public static IList<IList<MobModel>> MobThemes = new List<IList<MobModel>>
        {
            new List<MobModel> { SkeletonArcher, SkeletonWarrior, SkeletonRogue, SkeletonKnight, SkeletonWarlock},
            new List<MobModel> {Spider, SpiderRed, SpiderGreen, CatBlack, SnakePurple, SnakeGreen, ScorpionPale, ScorpionBlack, Bat, BatDark},
            new List<MobModel> {GoblinWarrior, GoblinArmouredShieldedWarrior, GoblingArmouredWarrior, GoblinMage},
            
        };
    }
}