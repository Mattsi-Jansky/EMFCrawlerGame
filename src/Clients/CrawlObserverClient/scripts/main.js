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
game.graphics.keys.hatsSheet = "hats";
game.graphics.keys.objects1Sheet = "objects1";
game.graphics.keys.objects3Sheet = "objects3";
game.graphics.keys.objects4Sheet = "objects4";
game.graphics.addRequest = [
    { name: game.graphics.keys.envSheet, url: 'assets/graphics/env.png'},
    { name: game.graphics.keys.charSheet, url: 'assets/graphics/characters.png'},
    { name: game.graphics.keys.mobSheet, url: 'assets/graphics/mobs.png'},
    { name: game.graphics.keys.hatsSheet, url: 'assets/graphics/hats.png'}, //the most important sprite sheet by far
    { name: game.graphics.keys.objects1Sheet, url: 'assets/graphics/objs.png'},
    { name: game.graphics.keys.objects3Sheet, url: 'assets/graphics/objs3.png'},
    { name: game.graphics.keys.objects4Sheet, url: 'assets/graphics/objs4.png'},
];

game.var.init = function() {
    game.var.xSize = 900;
    game.var.ySize = 600;
    game.var.spriteSize = 16;
    game.var.resolution = 1;
    game.var.tiles = [];
    game.var.framerateCounter = 0;
    game.var.timeStep = 1000 / 30;
    game.var.address = "wss://localhost:44349/observe/";
};

game.graphics.init = function() {
    game.graphics.sprites = [];
    game.graphics.init.environmentTiles();
    game.graphics.init.characters();
    game.graphics.init.mobs();
    game.graphics.init.objects();
};

game.graphics.init.environmentTiles = function() {
    var start = 403;

    for(var y = 0; y < 16; y++) {
        for(var x = 0; x < 16; x++) {
            var texture = TextureCache[game.graphics.keys.envSheet].clone();
            texture.frame = new Rectangle(x * game.var.spriteSize, y * game.var.spriteSize, game.var.spriteSize, game.var.spriteSize);
            game.graphics[start + (y * 16) + x] = texture;
        }
    }
};

game.graphics.init.characters = function() {
    var start = 0;

    for(var y = 0; y < 31; y++) {
        for(var x = 0; x < 13; x++) {
            var texture = TextureCache[game.graphics.keys.charSheet].clone();
            texture.frame = new Rectangle(x * game.var.spriteSize, y * game.var.spriteSize, game.var.spriteSize, game.var.spriteSize);
            game.graphics[start + (y * 13) + x] = texture;
        }
    }
};

game.graphics.init.mobs = function() {
    var start = 467;

    for(var y = 0; y < 15; y++) {
        for(var x = 0; x < 16; x++) {
            var texture = TextureCache[game.graphics.keys.mobSheet].clone();
            texture.frame = new Rectangle(x * game.var.spriteSize, y * game.var.spriteSize, game.var.spriteSize, game.var.spriteSize);
            game.graphics[start + (y * 16) + x] = texture;
        }
    }
};

game.graphics.init.objects = function() {
    var start = 707;
    var y = 0;
    var x = 0;

    for(y = 0; y < 2; y++) {
        for(x = 0; x < 11; x++) {
            var texture = TextureCache[game.graphics.keys.hatsSheet].clone();
            texture.frame = new Rectangle(x * game.var.spriteSize, y * game.var.spriteSize, game.var.spriteSize, game.var.spriteSize);
            game.graphics[start + (y * 11) + x] = texture;
        }
    }

    start += x * y;
    for(y = 0; y < 5; y++) {
        for(x = 0; x < 16; x++) {
            var texture = TextureCache[game.graphics.keys.objects1Sheet].clone();
            texture.frame = new Rectangle(x * game.var.spriteSize, y * game.var.spriteSize, game.var.spriteSize, game.var.spriteSize);
            game.graphics[start + (y * 16) + x] = texture;
        }
    }

    start += x * y;
    for(y = 0; y < 4; y++) {
        for(x = 0; x < 9; x++) {
            var texture = TextureCache[game.graphics.keys.objects3Sheet].clone();
            texture.frame = new Rectangle(x * game.var.spriteSize, y * game.var.spriteSize, game.var.spriteSize, game.var.spriteSize);
            game.graphics[start + (y * 9) + x] = texture;
        }
    }

    start += x * y;
    for(y = 0; y < 3; y++) {
        for(x = 0; x < 12; x++) {
            var texture = TextureCache[game.graphics.keys.objects4Sheet].clone();
            texture.frame = new Rectangle(x * game.var.spriteSize, y * game.var.spriteSize, game.var.spriteSize, game.var.spriteSize);
            game.graphics[start + (y * 12) + x] = texture;
        }
    }
}

game.network.init = function() {
    game.network.socket = new WebSocket(game.var.address);
    game.network.socket.onmessage = game.network.refresh;
};

game.network.refresh = function(event) {
    game.var.tiles = JSON.parse(event.data);
};

game.init = function() {
    game.var.init();
    game.menu.display.empty();
    PIXI.settings.SCALE_MODE = PIXI.SCALE_MODES.NEAREST;

    game.app = new PIXI.Application({
        width: 1920,
        height: 1080,
        backgroundColor : game.var.colours.background,
        transparent: true,
        clearBeforeRender: true,
        antialias: false,
        roundPixels: true,
        view: document.getElementById("main")
    });
    game.app.stage.scale.set(game.var.resolution, game.var.resolution);
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
    var texts = [];

    for(x = 0; x < game.var.tiles.length; x++) {
        var row = game.var.tiles[x];
        for(y = 0; y < row.length; y++) {
            var tilePositionX = (x * game.var.spriteSize);
            var tilePositionY = (y * game.var.spriteSize);
            var tile = row[y];

            tile.Graphics.forEach(function(i) {
                var graphic = new PIXI.Sprite(game.graphics[i]);
                graphic.x = tilePositionX;
                graphic.y = tilePositionY;
                game.app.stage.addChild(graphic);
            });

            if(tile.Text) {
                var text = new PIXI.Text(tile.Text, {
                    fontSize: 15,
                    fill: '#eeee',
                    stroke: '#111111',
                    strokeThickness: 3,
                    fontWeight: 'bold',
                    align: 'center',
                    wordWrap: true,
                    wordWrapWidth: game.var.spriteSize + game.var.spriteSize / 2
                });
                text.x = tilePositionX - game.var.spriteSize;
                text.y = tilePositionY + game.var.spriteSize;
                texts.push(text);
            }
        }
    }

    texts.forEach(function(text) {
        game.app.stage.addChild(text);
    });
};

$( document ).ready(function() {
    game.menu.display = $('#display-container');

    PIXI.loader
        .add(game.graphics.addRequest)
        .load(game.init);
});