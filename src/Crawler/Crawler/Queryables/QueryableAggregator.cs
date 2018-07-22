﻿using System;
using System.Collections.Generic;
using Crawler.Models;

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

        public void Add(T queryable)
        {
            _queryables.Add(queryable);
            queryable.AttachParent(this);
        }

        public void Remove(T queryable)
        {
            _queryables.Remove(queryable);
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
            var name = string.Empty;

            foreach (IQueryable queryable in _queryables)
            {
                name = queryable.GetDisplayText();
                if (!string.IsNullOrEmpty(name)) break;
            }

            return name;
        }

        public string GetName()
        {
            var name = string.Empty;

            foreach (var queryable in _queryables)
            {
                name = queryable.GetName();
                if (!string.IsNullOrEmpty(name)) break;
            }

            return name;
        }

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
