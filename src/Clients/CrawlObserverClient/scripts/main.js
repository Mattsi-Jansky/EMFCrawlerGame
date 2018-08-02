/**
 * 2018 Mattsi Jansky
 * Silly EMF project
 */

game = {};
game.menu = {};
game.var = {};
game.var.colours = {};
game.graphics = {};
game.network = {};

game.var.colours.background = 0x000000;

game.var.init = function() {
    game.var.xSize = 900;
    game.var.ySize = 600;
    game.var.scale = 8;
    game.var.tiles = [];
    game.var.delta = 0;
    game.var.timeStep = 1000 / 20;
    game.var.lastFrameTimestamp = 0;
};

game.graphics.init = function() {
    PIXI.SCALE_MODES.DEFAULT = PIXI.SCALE_MODES.NEAREST;
    game.graphics.test = PIXI.utils.TextureCache['assets/graphics/wall.png'];
};

game.network.init = function() {
    game.network.socket = new WebSocket("wss://localhost:44349/observe/");
    game.network.socket.onmessage = game.network.refresh;
};

game.network.refresh = function(event) {
    game.var.tiles = JSON.parse(event.data);
};

game.init = function() {
    game.var.init();
    game.menu.display.empty();

    game.app = new PIXI.Application(game.menu.display.width() / 4, game.menu.display.height() / 4, {
        backgroundColor : game.var.colours.background,
        transparent: true,
        clearBeforeRender: true,
        resolution: 4,
        antialias: false,
        roundPixels: true
    });
    game.menu.display.append(game.app.view);
    game.network.init();
    game.graphics.init();

    game.app.ticker.add(function(delta) {
        game.updateGame();
    });
};

game.updateGame = function() {
    game.app.stage.removeChildren();
    game.render();
};

game.render = function() {
    for(x = 0; x < game.var.tiles.length; x++) {
        var row = game.var.tiles[x];
        for(y = 0; y < row.length; y++) {
            var tilePositionX = (x * game.var.scale);
            var tilePositionY = (y * game.var.scale);
            var tile = row[y];
            
            if(tile.Graphics[0] > 390) {
                var wall = new PIXI.Sprite(game.graphics.test);
                wall.x = tilePositionX;
                wall.y = tilePositionY;
                game.app.stage.addChild(wall);
            }
        }
    }
};

$( document ).ready(function() {
    game.menu.display = $('#display-container');

    PIXI.loader
        .add('assets/graphics/wall.png')
        .load(game.init);
});