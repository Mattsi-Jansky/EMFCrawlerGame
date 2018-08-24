﻿using System.Collections.Generic;
using Crawler.Commands;
using Crawler.Models;
using Crawler.ObjectResolvers;

namespace Crawler.Queryables.Entities.Components
{
    public abstract class Component : IQueryable
    {
        protected IQueryable Parent { get; private set; }

        public virtual bool CanMove() { return true; }
        public virtual void GetGraphics(ref IList<Graphic> graphics) { }
        public virtual void GetPosition(ref Point? position) { }
        public virtual void SetPosition(Point position) { }
        public virtual string GetDisplayText() { return default(string); }
        public virtual string GetName() { return default(string); }
        public virtual ICommand GetCommand() { return default(ICommand); }
        public virtual void InitialiseController(ObjectResolver objectResolver) { }
        public virtual bool IsBlocked() { return default(bool); }
        public void RecieveMessage(string message) { }
        public IList<string> GetMessages() { return new List<string>(); }

        public void AttachParent(IQueryable parent)
        {
            Parent = parent;
        }

        public void DetatchParent()
        {
            Parent = null;
        }
    }
}