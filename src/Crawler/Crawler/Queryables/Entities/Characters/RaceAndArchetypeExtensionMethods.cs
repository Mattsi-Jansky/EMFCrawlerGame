using System;
using Crawler.Models;

namespace Crawler.Queryables.Entities.Characters
{
    public static class RaceAndArchetypeExtensionMethods
    {
        public static Graphic GetGraphic(this Race race, Archetype archetype)
        {
            int raceValue = (int) race;
            int archValue = (int) archetype;
            int numberOfArchetypes = Enum.GetNames(typeof(Archetype)).Length;
            int graphicValue = (raceValue * numberOfArchetypes) + archValue;

            return (Graphic) graphicValue;
        }
    }
}
