using System;
using System.Collections.Generic;
using Crawler.Commands;
using Crawler.Queryables.Entities;

namespace Crawler.Services
{
    public class MobCommandFetchingService
    {
        private readonly EntitiesCollection _entityCollection;

        public MobCommandFetchingService(EntitiesCollection entityCollection)
        {
            this._entityCollection = entityCollection;
        }

        public IDictionary<Guid, ICommand> FetchCommands()
        {
            IDictionary<Guid, ICommand> commands = new Dictionary<Guid, ICommand>();

            foreach (var entity in _entityCollection.Get())
            {
                var command = entity.GetCommand();
                if(command != null) commands[entity.Id] = command;
            }

            return commands;
        }
    }
}