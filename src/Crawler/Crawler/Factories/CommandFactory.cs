using System;
using System.Collections.Generic;
using Crawler.Commands;
using Crawler.ObjectResolvers;

namespace Crawler.Factories
{
    public class CommandFactory
    {
        private readonly Guid _id;
        private ObjectResolver _objectResolver;

        public CommandFactory(Guid id, ObjectResolver objectResolver)
        {
            _id = id;
            this._objectResolver = objectResolver;
        }


        public ICommand Move(Point targetPosition)
        {
            return _objectResolver.Resolve<MoveCommand>(new Dictionary<string, object>
            {
                {"id", _id},
                {"direction", targetPosition}
            });
        }
    }
}