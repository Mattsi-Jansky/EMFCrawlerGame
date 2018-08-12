using System;
using System.Collections.Generic;
using System.Linq;
using Crawler.Commands;
using Crawler.Factories;
using Crawler.Maps;
using Crawler.Maps.EntityPlacers;
using Crawler.Maps.Initialisers;
using Crawler.ObjectResolvers;
using Crawler.Observers;
using Crawler.Queryables.Entities;
using Crawler.Services;

namespace Crawler
{
    public class CrawlGame
    {
        private readonly IMap _map;
        private Dictionary<Guid, ICommand> _commands;
        private readonly Object _commandsLock = new Object();
        private readonly Dictionary<Guid, CommandFactory> _commandFactories;
        private readonly Object _commandFactoriesLock = new Object();
        private readonly ObjectResolver _objectResolver;
        
        public ICrawlObserver Observer { get; }
        public AddCharactersService AddCharactersService { get; }

        public CrawlGame(BaseMapInitialiser mapInitialiser, IEntityPlacer entityPlacer)
        {
            _commands = new Dictionary<Guid, ICommand>();
            var entitiesCollection = new EntitiesCollection();
            _map = mapInitialiser.Initialise(entitiesCollection);
            Observer = new ConcurrentCrawlObserverWrapper();
            Observer.Update(_map);
            _commandFactories = new Dictionary<Guid, CommandFactory>();
            _objectResolver = new ObjectResolver();
            _objectResolver.Initialise(_map, entityPlacer, entitiesCollection);
            AddCharactersService = _objectResolver.Resolve<AddCharactersService>();
        }

        public void AddCommand(Guid id, ICommand command)
        {
            lock (_commandsLock)
            {
                _commands[id] = command;
            }
        }

        public void Tick()
        {
            AddCharactersService.Update();
            Observer.Update(_map);

            IList<ICommand> commands;
            lock (_commandsLock)
            {
                commands = _commands.Values.ToList();
                _commands = new Dictionary<Guid, ICommand>();
            }

            foreach (var command in commands)
            {
                if (command.IsValid())
                {
                    command.Resolve();
                    Observer.Update(_map);
                }
            }
        }

        public CommandFactory GetCommandFactory(Guid id)
        {
            lock (_commandFactoriesLock)
            {
                if (_commandFactories.ContainsKey(id)) return _commandFactories[id];
                else
                {
                    _commandFactories[id] = new CommandFactory(id, _objectResolver);
                    return _commandFactories[id];
                }
            }
        }
    }
}