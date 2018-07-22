using Crawler.Models;
using System.Collections.Generic;

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
        void Detatchparent();
    }
}
