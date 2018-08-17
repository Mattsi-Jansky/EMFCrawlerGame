using Crawler.Queryables.Entities.Characters;

namespace Crawler.Models
{
    public class NewCharacterRequest
    {
        public Race Race { get; }
        public Archetype Archetype { get; }
        public string Name { get; }

        public NewCharacterRequest(Race race, Archetype archetype, string name)
        {
            Race = race;
            Archetype = archetype;
            Name = name;
        }

        public NewCharacterRequest() { }
    }
}
