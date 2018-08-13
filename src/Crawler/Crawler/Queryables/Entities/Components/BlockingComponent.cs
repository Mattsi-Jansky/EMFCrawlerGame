namespace Crawler.Queryables.Entities.Components
{
    public class BlockingComponent : Component
    {
        public override bool IsBlocked()
        {
            return true;
        }
    }
}