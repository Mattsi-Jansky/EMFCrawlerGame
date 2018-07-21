using System;
using System.Collections.Generic;
using System.Text;

namespace Crawler.Queryables.Entities.Components
{
    public class CharacterComponent : Component
    {
        private string _name;

        public CharacterComponent(string name)
        {
            _name = name;
        }

        public override string GetName()
        {
            return _name;
        }
    }
}
