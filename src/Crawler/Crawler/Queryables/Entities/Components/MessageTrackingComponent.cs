using System.Collections.Generic;
using System.Linq;

namespace Crawler.Queryables.Entities.Components
{
    public class MessageTrackingComponent : Component
    {
        private IList<string> _messages;

        public MessageTrackingComponent()
        {
            _messages = new List<string>();
        }

        public override void RecieveMessage(string message)
        {
            _messages.Add(message);
        }

        public override IList<string> GetMessages()
        {
            var result = _messages.ToList();
            _messages.Clear();
            return result;
        }
    }
}