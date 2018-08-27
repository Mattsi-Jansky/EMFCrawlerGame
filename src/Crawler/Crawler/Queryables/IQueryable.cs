using Crawler.Models;
using System.Collections.Generic;
using Crawler.Commands;
using Crawler.ObjectResolvers;
using Crawler.Queryables.Entities;
using Crawler.Queryables.Entities.Components;

namespace Crawler.Queryables
{
    public interface IQueryable
    {
        bool CanMove();
        void GetGraphics(ref IList<Graphic> graphics);
        void GetPosition(ref Point? position);
        void SetPosition(Point position);
        string GetDisplayText();
        string GetName();
        void AttachParent(IQueryable parent);
        void DetatchParent();
        ICommand GetCommand();
        void InitialiseController(ObjectResolver objectResolver);
        bool IsBlocked();
        void RecieveMessage(string message);
        IList<string> GetMessages();
        void GetInteractableEntities(ref IList<Entity> entities);
        EquipableComponent GetEquipable(EquipableSlot slot);
        void GetEquipables(ref List<EquipableComponent> equipables);
        string GetPreTitle();
        string GetPostTitle();
    }
}
