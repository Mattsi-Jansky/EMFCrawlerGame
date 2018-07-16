namespace Crawler.Queryables.Entities.Components
{
    public class PositionComponent : Component
    {
        private Point _position;

        public override void GetPosition(ref Point? position)
        {
            position = _position;
        }

        public override void SetPosition(Point position)
        {
            _position = position;
        }
    }
}
