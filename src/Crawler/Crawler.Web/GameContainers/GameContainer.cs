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

        public const int GameLoopTickTime = 250;
        public const int CleanupClientsLoopTickTime = 30000;
        private readonly Stopwatch _gameLoopTimer;
        private readonly CrawlGame _game;
        private readonly Thread _gameLoop;
        private readonly Stopwatch _cleanupClientLoopTimer;
        private readonly Thread _cleanupClientLoop;
        private readonly ClientTrackingService _clientTrackingService;
        private bool _running;

        private GameContainer()
        {
            _running = true;
            _gameLoopTimer = new Stopwatch();
            _gameLoopTimer.Start();
            _game = new CrawlGame(new DungeonMapInitialiser(new Random()), new RandomEntityPlacer());
            _gameLoop = new Thread(GameLoop);
            _gameLoop.Start();
            _clientTrackingService = new ClientTrackingService(_game);
            _cleanupClientLoopTimer = new Stopwatch();
            _cleanupClientLoop = new Thread(CleanupClientsLoop);
            _cleanupClientLoop.Start();
        }

        private void GameLoop()
        {
            while (_running)
            {
                if(_gameLoopTimer.ElapsedMilliseconds >= GameLoopTickTime)
                {
                    _gameLoopTimer.Reset();
                    _game.Tick();
                    _gameLoopTimer.Start();
                }
            }
        }

        private void CleanupClientsLoop()
        {
            while (_running)
            {
                if (_cleanupClientLoopTimer.ElapsedMilliseconds >= CleanupClientsLoopTickTime)
                {
                    _cleanupClientLoopTimer.Reset();
                    _clientTrackingService.RemoveOldClients();
                    _cleanupClientLoopTimer.Start();
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
            _gameLoop.Join();
            _cleanupClientLoop.Join();
        }
    }
}
