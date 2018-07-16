using Crawler.Queryables.Entities.Characters;

namespace Crawler.Models
{
    public class NewCharacterRequest
    {
        public Race Race { get; private set; }
        public Archetype Archetype { get; private set; }

        public NewCharacterRequest(Race race, Archetype archetype)
        {
            Race = race;
            Archetype = archetype;
        }
    }
}
