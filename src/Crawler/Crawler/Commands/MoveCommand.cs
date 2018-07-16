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
            return _direction.AbsSum() == 1;
        }
        
        public void Resolve()
        {
            var entity = _entitiesCollection.Get(_id);
            Point position = entity.GetPosition();
            var targetPosition = position.Add(_direction);
            _moveEntityService.Move(entity, targetPosition);
        }
    }
}