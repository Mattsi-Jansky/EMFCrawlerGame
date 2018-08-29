using System;
using Crawler.Factories;
using Crawler.Maps;
using Crawler.ObjectResolvers;
using Crawler.Services;

namespace Crawler.Queryables.Entities.Components.Controllers
{
    public class ControllerComponent : Component
    {
        protected readonly Guid Id;
        protected CommandFactory CommandFactory;
        protected MobQueryService MobQueryService;

        public ControllerComponent(Guid id)
        {
            Id = id;
        }

        public override void InitialiseController(ObjectResolver objectResolver)
        {
            CommandFactory = new CommandFactory(Id, objectResolver);
            MobQueryService = new MobQueryService(objectResolver.Resolve<IMap>(), (Entity) Parent);
        }
    }
}