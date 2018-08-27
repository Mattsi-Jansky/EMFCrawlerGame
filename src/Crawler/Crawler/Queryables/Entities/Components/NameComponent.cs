using System;
using System.Collections.Generic;
using System.Text;

namespace Crawler.Queryables.Entities.Components
{
    public class NameComponent : Component
    {
        private string _name;

        public NameComponent(string name)
        {
            _name = name;
        }

        public override string GetName()
        {
            return _name;
        }
    }
}
