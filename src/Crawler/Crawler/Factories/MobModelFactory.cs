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
        
        public static IList<IList<MobModel>> MobThemes = new List<IList<MobModel>>
        {
            new List<MobModel> { SkeletonArcher, SkeletonWarrior, SkeletonRogue, SkeletonKnight, SkeletonWarlock}
        };
    }
}