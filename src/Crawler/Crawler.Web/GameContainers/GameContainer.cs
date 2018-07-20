﻿using System;
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

        public const int TickTime = 250;
        private readonly Stopwatch _timer;
        private readonly CrawlGame _game;
        private readonly Thread _gameLoop;
        private bool _running;

        private GameContainer()
        {
            _running = true;
            _timer = new Stopwatch();
            _timer.Start();
            _game = new CrawlGame(new WalledBlankMapInitialiser(new Point(10,10)), new RandomEntityPlacer());
            _gameLoop = new Thread(Loop);
            _gameLoop.Start();
        }

        private void Loop()
        {
            while (_running)
            {
                if(_timer.ElapsedMilliseconds >= TickTime)
                {
                    _timer.Reset();
                    _game.Tick();
                    _timer.Start();
                }
            }
        }

        public Graphic[][][] GetGraphicState()
        {
            return _game.Observer.Observe();
        }

        public void Dispose()
        {
            _running = false;
            Thread.Sleep(GameContainer.TickTime * 2);
            _gameLoop.Join();
        }
    }
}
