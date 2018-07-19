using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Crawler.Maps.EntityPlacers;
using Crawler.Maps.Initialisers;
using Crawler.Models;

namespace Crawler.Web.GameContainers
{
    public class GameContainer : IDisposable
    {
        private static GameContainer _this;
        public static GameContainer Instance => _this ?? (_this = new GameContainer());

        private const int tickTime = 250;
        private readonly Stopwatch _timer;
        private CrawlGame _game;
        private Thread _gameLoop;

        public GameContainer()
        {
            _timer = new Stopwatch();
            _timer.Start();
            _game = new CrawlGame(new WalledBlankMapInitialiser(new Point(10,10)), new RandomEntityPlacer());
            _gameLoop = new Thread(Tick);
            _gameLoop.Start();
        }

        private void Tick()
        {
            while (_timer.ElapsedMilliseconds < tickTime)
            {
                _timer.Reset();
                _game.Tick();
            }
        }

        public Graphic[][][] GetGraphicState()
        {
            return _game.Observer.Observe();
        }

        public void Dispose()
        {
            _gameLoop.Join();
        }
    }
}
