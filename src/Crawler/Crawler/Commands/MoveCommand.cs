using System;
using Crawler.Queryables.Entities;
using Crawler.Services;

namespace Crawler.Commands
{
    public class MoveCommand : ICommand
    {
        private Guid _id;
        private Point _direction;
        private EntitiesCollection _entitiesCollection;
        private readonly MoveEntityService _moveEntityService;
        private Entity _entity;
        private Point _targetPosition;

        public MoveCommand(Guid id, Point direction, EntitiesCollection entitiesCollection, 
            MoveEntityService moveEntityService)
        {
            _id = id;
            _direction = direction;
            _entitiesCollection = entitiesCollection;
            _moveEntityService = moveEntityService;
        }

        public bool IsValid()
        {
            if (!_entitiesCollection.Contains(_id)) return false;
            
            _entity = _entitiesCollection.Get(_id);
            Point position = _entity.GetPosition();
            _targetPosition = position.Add(_direction);
            
            return _direction.AbsSum() == 1
                && _moveEntityService.IsLegalTileToOccupy(_targetPosition);
        }
        
        public void Resolve()
        {
            _moveEntityService.Move(_entity, _targetPosition);
        }
    }
}