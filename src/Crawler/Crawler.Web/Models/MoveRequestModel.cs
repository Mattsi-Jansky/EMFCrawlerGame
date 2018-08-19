using System;

namespace Crawler.Web.Models
{
    public class MoveRequestModel
    {
        public Guid Id { get; set; }
        public Point Direction { get; set; }
    }
}