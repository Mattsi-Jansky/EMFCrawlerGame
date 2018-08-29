using Crawler.Queryables.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crawler.Services
{
    public class PlayerClientMessagesService
    {
        private object _messagesLock = new Object();
        private Dictionary<Guid, List<string>> _messages;
        private EntitiesCollection _entitiesManager;

        public PlayerClientMessagesService(EntitiesCollection entitiesManager)
        {
            _messages = new Dictionary<Guid, List<string>>();
            _entitiesManager = entitiesManager;
        }

        public IList<string> GetMessages(Guid id)
        {
            lock(_messagesLock)
            {
                var result = _messages[id];
                _messages[id] = new List<string>();
                return result;
            }
        }

        public void Update()
        {
            lock(_messagesLock)
            {
                var deadEntities = new List<Entity>();
                
                foreach(var entity in _entitiesManager.Get())
                {
                    if (_messages.ContainsKey(entity.Id))
                    {
                        var messages = entity.GetMessages();
                        _messages[entity.Id].AddRange(messages);
                    }
                    if(entity.IsDead()) deadEntities.Add(entity);
                }
                
                //This is done here because entities need to receive their final death message
                //before being removed
                foreach (var deadEntity in deadEntities)
                {
                    _entitiesManager.Remove(deadEntity.Id);
                }
            }
        }

        public void Add(Guid id)
        {
            lock(_messagesLock)
            {
                _messages[id] = new List<string>();
            }
        }

        public void Remove(Guid id)
        {
            lock(_messagesLock)
            {
                _messages.Remove(id);
            }
        }
    }
}
