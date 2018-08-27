using System;
using System.Collections.Generic;
using System.Text;

namespace Crawler.Queryables.Entities.Components
{
    public class CharacterNameDisplayTextComponent : Component
    {
        public override string GetDisplayText()
        {
            return $"{Parent.GetPreTitle()} {Parent.GetName()} {Parent.GetPostTitle()}";
        }
    }
}
