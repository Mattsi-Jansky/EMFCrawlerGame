using System.Collections.Generic;
using Crawler.Models;

namespace Crawler.Queryables.Entities.Components
{
    public class AnimatedGraphicComponent : Component
    {
        private Graphic[] frames;
        private int currentFrame;

        public AnimatedGraphicComponent(Graphic[] frames)
        {
            this.frames = frames;
            currentFrame = 0;
        }

        public override void GetGraphics(ref IList<Graphic> graphics)
        {
            graphics.Add(frames[currentFrame]);
            
            if (currentFrame + 1 == frames.Length) currentFrame = 0;
            else currentFrame++;
        }
    }
}