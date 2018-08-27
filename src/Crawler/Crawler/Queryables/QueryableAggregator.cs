﻿using System;
using System.Collections.Generic;
using Crawler.Commands;
using Crawler.Factories;
using Crawler.Models;
using Crawler.ObjectResolvers;
using Crawler.Queryables.Entities;
using Crawler.Queryables.Entities.Components;

namespace Crawler.Queryables
{
    public class QueryableAggregator<T> : Component where T : IQueryable
    {
        protected readonly IList<IQueryable> Queryables;

        public QueryableAggregator()
        {
            Queryables = new List<IQueryable>();
        }

        public QueryableAggregator<T> Add(T queryable)
        {
            Queryables.Add(queryable);
            queryable.AttachParent(this);
            return this;
        }

        public QueryableAggregator<T> Remove(T queryable)
        {
            Queryables.Remove(queryable);
            queryable.DetatchParent();
            return this;
        }

        public override  bool CanMove()
        {
            bool canMove = true;

            foreach(IQueryable queryable in Queryables)
            {
                canMove = canMove && queryable.CanMove();
            }

            return canMove;
        }

        public override  void GetGraphics(ref IList<Graphic> graphics)
        {
            foreach (IQueryable queryable in Queryables)
            {
                queryable.GetGraphics(ref graphics);
            }
        }

        public override  void GetPosition(ref Point? position)
        {
            foreach (IQueryable queryable in Queryables)
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

        public override  void SetPosition(Point position)
        {
            foreach (IQueryable queryable in Queryables)
            {
                queryable.SetPosition(position);
            }
        }

        public override string GetDisplayText()
        {
            foreach (IQueryable queryable in Queryables)
            {
                var result = queryable.GetDisplayText();
                if (!string.IsNullOrEmpty(result)) return result;
            }

            return string.Empty;
        }

        public override string GetName()
        {
            foreach (var queryable in Queryables)
            {
                var result = queryable.GetName();
                if (!string.IsNullOrEmpty(result)) return result;
            }

            return string.Empty;
        }

        public override ICommand GetCommand()
        {
            foreach (var queryable in Queryables)
            {
                var result = queryable.GetCommand();
                if (result != null) return result;
            }

            return default(ICommand);
        }

        public override void InitialiseController(ObjectResolver objectResolver)
        {
            foreach (var queryable in Queryables)
            {
                queryable.InitialiseController(objectResolver);
            }
        }

        public override bool IsBlocked()
        {
            bool isBlocked = false;

            foreach(IQueryable queryable in Queryables)
            {
                isBlocked = isBlocked || queryable.IsBlocked();
            }

            return isBlocked;
        }

        public override void RecieveMessage(string message)
        {
            foreach (IQueryable queryable in Queryables)
            {
                queryable.RecieveMessage(message);
            }
        }

        public override IList<string> GetMessages()
        {
            var messages = new List<string>();

            foreach (IQueryable queryable in Queryables)
            {
                messages.AddRange(queryable.GetMessages());
            }

            return messages;
        }

        public override void GetInteractableEntities(ref IList<Entity> entities)
        {
            foreach (IQueryable queryable in Queryables)
            {
                queryable.GetInteractableEntities(ref entities);
            }
        }

        public override EquipableComponent GetEquipable(EquipableSlot slot)
        {
            foreach (IQueryable queryable in Queryables)
            {
                var result = queryable.GetEquipable(slot);
                if (result != null) return result;
            }

            return null;
        }

        public override void GetEquipables(ref List<EquipableComponent> equipables)
        {
            foreach (var queryable in Queryables)
            {
                queryable.GetEquipables(ref equipables);
            }
        }
    }
}
