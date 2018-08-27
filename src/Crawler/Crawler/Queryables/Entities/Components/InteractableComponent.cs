using System.Collections.Generic;

namespace Crawler.Queryables.Entities.Components
{
    public class InteractableComponent : Component
    {
        public override void GetInteractableEntities(ref IList<Entity> entities)
        {
            //Woops, breaking Liskov Substitution...
            if (Parent is Entity)
            {
                entities.Add((Entity)Parent);
            }
        }
    }
}