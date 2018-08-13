using Crawler.Models;
using System.Collections.Generic;
using Crawler.Commands;
using Crawler.ObjectResolvers;

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
    }
}
