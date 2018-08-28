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
    public class PlayerCharactersService
    {
        private readonly Stack<Entity> _newCharacters;
        private readonly Object _newCharactersLock = new Object();
        private readonly Stack<Guid> _deleteRequestedCharacterIds;
        private readonly Object _deleteRequestedCharacterIdsLock = new Object();
        private readonly PutEntityService _putEntityService;
        private readonly EntitiesCollection _entities;
        private readonly IEntityPlacer _entityPlacer;
        private readonly IMap _map;
        private readonly PlayerClientMessagesService _playerClientMessagesService;

        public PlayerCharactersService(PutEntityService entityService, EntitiesCollection entities, IEntityPlacer entityPlacer, IMap map, PlayerClientMessagesService playerClientMessagesService)
        {
            _putEntityService = entityService;
            _entities = entities;
            _entityPlacer = entityPlacer;
            _map = map;
            _newCharacters = new Stack<Entity>();
            _deleteRequestedCharacterIds = new Stack<Guid>();
            _playerClientMessagesService = playerClientMessagesService;
        }

        public Guid Add(NewCharacterRequest newCharacterRequest)
        {
            var entity = new Entity();
            entity.Add(new GraphicComponent(newCharacterRequest.Race.GetGraphic(newCharacterRequest.Archetype)));
            entity.Add(new PositionComponent());
            entity.Add(new NameComponent(newCharacterRequest.Name));
            entity.Add(new CharacterNameDisplayTextComponent());
            entity.Add(new BlockingComponent());
            entity.Add(new MessageTrackingComponent());
            entity.Add(new CharacterComponent(
                CharacterStats.GetStatsFor(newCharacterRequest.Race, newCharacterRequest.Archetype)));

            lock (_newCharactersLock)
            {
                _newCharacters.Push(entity);
            }

            return entity.Id;
        }
        
        public void Delete(Guid id)
        {
            lock (_deleteRequestedCharacterIdsLock)
            {
                _deleteRequestedCharacterIds.Push(id);
            }
        }

        public void Update()
        {
            lock (_newCharactersLock)
            {
                while (_newCharacters.Count > 0)
                {
                    AddCharacter(_newCharacters.Pop());
                }
            }

            lock (_deleteRequestedCharacterIdsLock)
            {
                while (_deleteRequestedCharacterIds.Count > 0)
                {
                    DeleteCharacter(_deleteRequestedCharacterIds.Pop());
                }
            }
        }

        private void AddCharacter(Entity character)
        {
            var position = _entityPlacer.PlaceEntity(_map, character);
            _putEntityService.Put(character, position);
            _entities.Add(character.Id, character);
            _playerClientMessagesService.Add(character.Id);

        }
        
        private void DeleteCharacter(Guid id)
        {
            var entity = _entities.Get(id);
            _map.Remove(entity, entity.GetPosition());
            _entities.Remove(id);
            _playerClientMessagesService.Remove(id);
        }
    }
}
