using System;
using Crawler.Factories;
using Crawler.ObjectResolvers;

namespace Crawler.Queryables.Entities.Components.Controllers
{
    public class ControllerComponent : Component
    {
        protected readonly Guid Id;
        protected CommandFactory CommandFactory;

        public ControllerComponent(Guid id)
        {
            Id = id;
        }

        public override void InitialiseController(ObjectResolver objectResolver)
        {
            CommandFactory = new CommandFactory(Id, objectResolver);
        }
    }
}