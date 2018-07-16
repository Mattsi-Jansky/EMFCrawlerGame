using Crawler.Maps;
using Crawler.Maps.Initialisers;

namespace Crawler.Tests.Support
{
    public class TestBlankMapInitialiser : BaseMapInitialiser
    {
        public override IMap Initialise()
        {
            return new Map(new Point(10, 10));
        }
    }
}