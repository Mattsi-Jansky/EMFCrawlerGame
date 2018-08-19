using Crawler.Queryables.Entities.Characters;

namespace Crawler.Models
{
    public class NewCharacterRequest
    {
        public Race Race { get; set; }
        public Archetype Archetype { get; set; }
        public string Name { get; set; }

        public NewCharacterRequest(Race race, Archetype archetype, string name)
        {
            Race = race;
            Archetype = archetype;
            Name = name;
        }

        public NewCharacterRequest() { }
    }
}
