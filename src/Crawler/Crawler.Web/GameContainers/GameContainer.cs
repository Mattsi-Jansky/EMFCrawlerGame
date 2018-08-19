using System;
using System.Diagnostics;
using System.Threading;
using Crawler.Commands;
using Crawler.Factories;
using Crawler.Maps.EntityPlacers;
using Crawler.Maps.Initialisers.DungeonGenerators;
using Crawler.Models;
using Crawler.Web.Models;
using Crawler.Web.Services;

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
        private readonly ClientTrackingService _clientTrackingService;
        private bool _running;

        private GameContainer()
        {
            _running = true;
            _timer = new Stopwatch();
            _timer.Start();
            _game = new CrawlGame(new DungeonMapInitialiser(new Random()), new RandomEntityPlacer());
            _gameLoop = new Thread(Loop);
            _gameLoop.Start();
            _clientTrackingService = new ClientTrackingService(_game);
        }

        private void Loop()
        {
            while (_running)
            {
                if(_timer.ElapsedMilliseconds >= TickTime)
                {
                    _timer.Reset();
                    _game.Tick();
                    _clientTrackingService.RemoveOldClients();
                    _timer.Start();
                }
            }
        }

        public TileGraphics[][] GetGraphicState()
        {
            return _game.Observer.Observe();
        }

        public CommandFactory GetCommandFactory(Guid id)
        {
            return _game.GetCommandFactory(id);
        }

        public void AddCommand(Guid id, ICommand command)
        {
            _game.AddCommand(id, command);
            _clientTrackingService.UpdateTimeout(id);
        }

        public Guid AddCharater(NewCharacterRequest request)
        {
            var id = _game.PlayerCharactersService.Add(request);
            _clientTrackingService.UpdateTimeout(id);
            return id;
        }

        public bool CanAddCharacter()
        {
            return !_clientTrackingService.HasReachedMaxClients();
        }

        public PlayerStatusModel GetStatus(Guid id)
        {
            _clientTrackingService.UpdateTimeout(id);
            return new PlayerStatusModel {Messages = new string[0]};
        }

        public bool ValidateClientId(Guid id)
        {
            return _clientTrackingService.Contains(id);
        }

        public void Dispose()
        {
            _running = false;
            Thread.Sleep(GameContainer.TickTime * 2);
            _gameLoop.Join();
        }
    }
}
