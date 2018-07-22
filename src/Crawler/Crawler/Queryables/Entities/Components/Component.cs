using System.Collections.Generic;
using Crawler.Models;

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
        public void AttachParent(IQueryable parent)
        {
            Parent = parent;
        }

        public void Detatchparent()
        {
            Parent = null;
        }
    }
}