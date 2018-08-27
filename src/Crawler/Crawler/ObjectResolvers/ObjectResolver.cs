using System;
using System.Collections.Generic;
using Crawler.Commands;
using Crawler.Maps;
using Crawler.Maps.EntityPlacers;
using Crawler.Queryables.Entities;
using Crawler.Services;
using Unity;

namespace Crawler.ObjectResolvers
{
    public class ObjectResolver
    {
        private IUnityContainer _container;
        private string CannotResolveUnregisteredTypeError = "Cannot resolve unregistered type";

        public ObjectResolver()
        {
            
        }

        public void Initialise(IMap map, IEntityPlacer entityPlacer, EntitiesCollection entitiesCollection)
        {
            _container?.Dispose();

            _container = new UnityContainer();
            _container.RegisterInstance(map);
            _container.RegisterInstance(entityPlacer);
            _container.RegisterInstance(entitiesCollection);
            _container.RegisterType(typeof(PlayerClientMessagesService));
            _container.RegisterInstance(_container.Resolve<PlayerClientMessagesService>());

            //todo should these be register instance?
            _container.RegisterType(typeof(PutEntityService));
            _container.RegisterType(typeof(MoveEntityService));
            _container.RegisterType(typeof(PlayerCharactersService));
            _container.RegisterType(typeof(MobCommandFetchingService));
            _container.RegisterType(typeof(InteractionService));

            _container.RegisterType(typeof(MoveCommand));
        }

        public T Resolve<T>()
        {
            if (!_container.IsRegistered<T>())
            {
                throw new ArgumentException(CannotResolveUnregisteredTypeError);
            }

            return _container.Resolve<T>();
        }

        public T Resolve<T>(Dictionary<string, Object> parameters)
        {
            if (!_container.IsRegistered<T>())
            {
                throw new ArgumentException(CannotResolveUnregisteredTypeError);
            }

            var overrides = new ParameterOverrides();
            foreach (var param in parameters)
            {
                overrides.Add(param.Key, param.Value);
            }

            return _container.Resolve<T>(overrides);
        }
    }
}
