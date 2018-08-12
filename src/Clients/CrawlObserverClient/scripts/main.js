/**
 * 2018 Mattsi Jansky
 * Silly EMF project
 */

//Aliases
let Application = PIXI.Application,
    Container = PIXI.Container,
    loader = PIXI.loader,
    resources = PIXI.loader.resources,
    TextureCache = PIXI.utils.TextureCache,
    Sprite = PIXI.Sprite,
    Rectangle = PIXI.Rectangle;

var debug = true;

game = {};
game.menu = {};
game.var = {};
game.var.colours = {};
game.graphics = {};
game.graphics.keys = {};
game.network = {};

game.var.colours.background = 0x000000;
game.graphics.keys.envSheet = "env";
game.graphics.keys.charSheet = "char";
game.graphics.keys.mobSheet = "mob";
game.graphics.addRequest = [
    { name: game.graphics.keys.envSheet, url: 'assets/graphics/env.png'},
    { name: game.graphics.keys.charSheet, url: 'assets/graphics/characters.png'},
    { name: game.graphics.keys.mobSheet, url: 'assets/graphics/mobs.png'}
];

game.var.init = function() {
    game.var.xSize = 900;
    game.var.ySize = 600;
    game.var.scale = 8;
    game.var.tiles = [];
    game.var.framerateCounter = 0;
    game.var.timeStep = 1000 / 30;
};

game.graphics.init = function() {
    PIXI.scaleModes.DEFAULT = PIXI.SCALE_MODES.NEAREST;

    game.graphics.sprites = [];
    game.graphics.init.environmentTiles();
    game.graphics.init.characters();
    game.graphics.init.mobs();
};

game.graphics.init.environmentTiles = function() {
    var start = 403;

    for(var y = 0; y < 16; y++) {
        for(var x = 0; x < 16; x++) {
            var texture = TextureCache[game.graphics.keys.envSheet].clone();
            texture.frame = new Rectangle(x * 8, y * 8, 8, 8);
            game.graphics[start + (y * 16) + x] = texture;
        }
    }
};

game.graphics.init.characters = function() {
    var start = 0;

    for(var y = 0; y < 31; y++) {
        for(var x = 0; x < 13; x++) {
            var texture = TextureCache[game.graphics.keys.charSheet].clone();
            texture.frame = new Rectangle(x * 8, y * 8, 8, 8);
            game.graphics[start + (y * 13) + x] = texture;
        }
    }
};

game.graphics.init.mobs = function() {
    var start = 467;

    for(var y = 0; y < 15; y++) {
        for(var x = 0; x < 16; x++) {
            var texture = TextureCache[game.graphics.keys.mobSheet].clone();
            texture.frame = new Rectangle(x * 8, y * 8, 8, 8);
            game.graphics[start + (y * 16) + x] = texture;
        }
    }
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

    game.app = new PIXI.Application(game.menu.display.width() / 2, game.menu.display.height() / 2, {
        backgroundColor : game.var.colours.background,
        transparent: true,
        clearBeforeRender: true,
        resolution: 2,
        antialias: false,
        roundPixels: true
    });
    game.menu.display.append(game.app.view);
    game.network.init();
    game.graphics.init();

    game.app.ticker.add(function(delta) {
        game.var.framerateCounter += delta;
        if(game.var.framerateCounter > game.var.timeStep) {
            game.updateGame();
        }
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

            tile.Graphics.forEach(function(i) {
                var graphic = new PIXI.Sprite(game.graphics[i]);
                graphic.x = tilePositionX;
                graphic.y = tilePositionY;
                game.app.stage.addChild(graphic);
            });
        }
    }
    debug = false;
};

$( document ).ready(function() {
    game.menu.display = $('#display-container');

    PIXI.loader
        .add(game.graphics.addRequest)
        .load(game.init);
});