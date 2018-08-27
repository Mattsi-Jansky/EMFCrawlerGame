using Crawler.Queryables.Entities;

namespace Crawler.Factories.ItemGenerators
{
    public interface IItemGenerator
    {
        Entity Generate();
    }
}