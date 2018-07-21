using System;
using System.Collections.Generic;
using Crawler.Maps;
using Crawler.Maps.EntityPlacers;
using Crawler.Models;
using Crawler.Queryables.Entities;
using Crawler.Queryables.Entities.Characters;
using Crawler.Queryables.Entities.Components;

namespace Crawler.Services
{
    public class AddCharactersService
    {
        private readonly Object _newCharactersLock = new Object();
        private readonly Stack<Entity> _newCharacters;
        private readonly PutEntityService _putEntityService;
        private readonly EntitiesCollection _entities;
        private readonly IEntityPlacer _entityPlacer;
        private readonly IMap _map;

        public AddCharactersService(PutEntityService entityService, EntitiesCollection entities, IEntityPlacer entityPlacer, IMap map)
        {
            _putEntityService = entityService;
            _entities = entities;
            _entityPlacer = entityPlacer;
            _map = map;
            _newCharacters = new Stack<Entity>();
        }

        public Guid Add(NewCharacterRequest newCharacterRequest)
        {
            var entity = new Entity();
            entity.Add(new GraphicComponent(newCharacterRequest.Race.GetGraphic(newCharacterRequest.Archetype)));
            entity.Add(new PositionComponent());
            entity.Add(new CharacterComponent(newCharacterRequest.Name));

            lock (_newCharactersLock)
            {
                _newCharacters.Push(entity);
            }

            return entity.Id;
        }

        public void Update()
        {
            lock (_newCharactersLock)
            {
                while (_newCharacters.Count > 0)
                {
                    var character = _newCharacters.Pop();
                    var position = _entityPlacer.PlaceCharacter(_map, character);
                    _putEntityService.Put(character, position);
                    _entities.Add(character.Id, character);
                }
            }
        }
    }
}
