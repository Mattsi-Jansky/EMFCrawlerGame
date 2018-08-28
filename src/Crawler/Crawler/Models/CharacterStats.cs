using System.Collections.Generic;
using Crawler.Queryables.Entities.Characters;
using Crawler.Services;

namespace Crawler.Models
{
    public struct CharacterStats
    {
        public static Dictionary<Race, CharacterStats> RaceStats = new Dictionary<Race, CharacterStats>
        {
            { Race.Barbarian, new CharacterStats(3, 1, 2, -1)},
            { Race.Cyclops, new CharacterStats(3, -1, 3, 0)},
            { Race.DarkElf, new CharacterStats(-1, 3, -1, 4)},
            { Race.Devilspawn, new CharacterStats(1, 3, 1, 0)},
            { Race.Drawconian, new CharacterStats(3, 1, 2, -1)},
            { Race.Dwarf, new CharacterStats(1, 0, 3, 1)},
            { Race.Elf, new CharacterStats(-1, 2, -1, 5)},
            { Race.Gnome, new CharacterStats(2, 3, -2, 2)},
            { Race.Golem, new CharacterStats(0, 0, 5, 0)},
            { Race.HalfElf, new CharacterStats(0, 2, 0, 3)},
            { Race.HalfGiant, new CharacterStats(2, 0, 4, -1)},
            { Race.HalfOgre, new CharacterStats(3, 0, 4, -2)},
            { Race.HalfOrc, new CharacterStats(3, 2, 1, -1)},
            { Race.HalfTitan, new CharacterStats(3, -2, 4, 0)},
            { Race.HalfTroll, new CharacterStats(3, 2, 2, -2)},
            { Race.HighElf, new CharacterStats(1, 2, -3, 5)},
            { Race.Hobbit, new CharacterStats(1, 4, -2, 2)},
            { Race.Human, new CharacterStats(1, 1, 2, 1)},
            { Race.Imp, new CharacterStats(2, 4, -2, 0)},
            { Race.Klackon, new CharacterStats(3, 4, 0, -2)},
            { Race.Kobold, new CharacterStats(1, 3, 1, 0)},
            { Race.MindFlayer, new CharacterStats(0, 0, 0, 5)},
            { Race.Nephilim, new CharacterStats(2, 3, 0, 0)},
            { Race.Nibelung, new CharacterStats(3, 0, 2, 0)},
            { Race.Saracen, new CharacterStats(1, 2, 1, 1)},
            { Race.Skeleton, new CharacterStats(2, 3, 0, 0)},
            { Race.Sprite, new CharacterStats(0, 5, -2, 2)},
            { Race.Vampire, new CharacterStats(0, 2, 0, 3)},
            { Race.Yeek, new CharacterStats(1, 4, 1, -1)},
            { Race.Zombie, new CharacterStats(2, 0, 7, -4)}  
        };
        
        public static Dictionary<Archetype, CharacterStats> ArchetypeStats = new Dictionary<Archetype, CharacterStats>
        {
            { Archetype.Warlock, new CharacterStats(2,0,-2,4)},
            { Archetype.HellKnight, new CharacterStats(3,-2,4,0)},
            { Archetype.Druid, new CharacterStats(-2,2,0,3)},
            { Archetype.HighMage, new CharacterStats(-2,0,-2,9)},
            { Archetype.Mage, new CharacterStats(0,0,-1,6)},
            { Archetype.Mindcrafter, new CharacterStats(0,-2,-2,9)},
            { Archetype.Mystic, new CharacterStats(1,2,-3,5)},
            { Archetype.Paladin, new CharacterStats(0,1,2,2)},
            { Archetype.Priest, new CharacterStats(2,1,-2,4)},
            { Archetype.Ranger, new CharacterStats(0,4,1,0)},
            { Archetype.Rogue, new CharacterStats(2,4,-1,0)},
            { Archetype.Warrior, new CharacterStats(3,2,2,-2)},
            { Archetype.WarriorMage, new CharacterStats(2,-2,2,3)},
        };

        public static CharacterStats GetStatsFor(Race race, Archetype archetype)
        {
            return RaceStats[race].Add(ArchetypeStats[archetype]);
        }
        
        public int str, dex, con, wis;

        public CharacterStats(int str, int dex, int con, int wis)
        {
            this.str = str;
            this.dex = dex;
            this.con = con;
            this.wis = wis;
        }

        private CharacterStats Add(CharacterStats b)
        {
            return new CharacterStats(str + b.str, dex + b.dex, con + b.con, wis + b.wis);
        }
    }
}