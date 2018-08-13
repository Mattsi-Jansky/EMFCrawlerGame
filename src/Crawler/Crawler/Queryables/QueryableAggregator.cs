using System;
using System.Collections.Generic;
using Crawler.Commands;
using Crawler.Factories;
using Crawler.Models;
using Crawler.ObjectResolvers;

namespace Crawler.Queryables
{
    public class QueryableAggregator<T> : IQueryable where T : IQueryable
    {
        private readonly IList<IQueryable> _queryables;
        protected IQueryable Parent { get; private set; }

        public QueryableAggregator()
        {
            _queryables = new List<IQueryable>();
        }

        public QueryableAggregator<T> Add(T queryable)
        {
            _queryables.Add(queryable);
            queryable.AttachParent(this);
            return this;
        }

        public QueryableAggregator<T> Remove(T queryable)
        {
            _queryables.Remove(queryable);
            queryable.DetatchParent();
            return this;
        }

        public virtual bool CanMove()
        {
            bool canMove = true;

            foreach(IQueryable queryable in _queryables)
            {
                canMove = canMove && queryable.CanMove();
            }

            return canMove;
        }

        public virtual void GetGraphics(ref IList<Graphic> graphics)
        {
            foreach (IQueryable queryable in _queryables)
            {
                queryable.GetGraphics(ref graphics);
            }
        }

        public virtual void GetPosition(ref Point? position)
        {
            foreach (IQueryable queryable in _queryables)
            {
                queryable.GetPosition(ref position);
                if (position.HasValue) break;
            }

            if(!position.HasValue) throw new InvalidOperationException("Entity has no position");
        }

        public virtual Point GetPosition()
        {
            Point? position = null;

            GetPosition(ref position);

            return position.Value;
        }

        public virtual void SetPosition(Point position)
        {
            foreach (IQueryable queryable in _queryables)
            {
                queryable.SetPosition(position);
            }
        }

        public string GetDisplayText()
        {
            foreach (IQueryable queryable in _queryables)
            {
                var result = queryable.GetDisplayText();
                if (!string.IsNullOrEmpty(result)) return result;
            }

            return string.Empty;
        }

        public string GetName()
        {
            foreach (var queryable in _queryables)
            {
                var result = queryable.GetName();
                if (!string.IsNullOrEmpty(result)) return result;
            }

            return string.Empty;
        }

        public void AttachParent(IQueryable parent)
        {
            Parent = parent;
        }

        public void DetatchParent()
        {
            Parent = null;
        }

        public ICommand GetCommand()
        {
            foreach (var queryable in _queryables)
            {
                var result = queryable.GetCommand();
                if (result != null) return result;
            }

            return default(ICommand);
        }

        public void InitialiseController(ObjectResolver objectResolver)
        {
            foreach (var queryable in _queryables)
            {
                queryable.InitialiseController(objectResolver);
            }
        }

        public bool IsBlocked()
        {
            bool isBlocked = false;

            foreach(IQueryable queryable in _queryables)
            {
                isBlocked = isBlocked || queryable.IsBlocked();
            }

            return isBlocked;
        }
    }
}
