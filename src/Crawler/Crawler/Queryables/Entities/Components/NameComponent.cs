using System;
using System.Collections.Generic;
using System.Text;

namespace Crawler.Queryables.Entities.Components
{
    public class NameComponent : Component
    {
        private string _name;
        private string _preTitle;
        private string _postTitle;
        
        public NameComponent(string name, string preTitle = "", string postTitle = "")
        {
            _name = name;
            _preTitle = preTitle;
            _postTitle = postTitle;
        }

        public override string GetName()
        {
            return _name;
        }

        public override string GetPreTitle()
        {
            return _preTitle;
        }

        public override string GetPostTitle()
        {
            return _postTitle;
        }
    }
}
