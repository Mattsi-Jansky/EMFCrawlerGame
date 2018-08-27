/**
 * 2018 Mattsi Jansky
 * Silly EMF project
 */

game = {};
game.menu = {};
game.menu.console = $('#console');
game.menu.up = $('#up');
game.menu.down = $('#down');
game.menu.left = $('#left');
game.menu.right = $('#right');
game.menu.name = $('#name');
game.menu.race = $('#race');
game.menu.archetype = $('#archetype');
game.menu.start = $('#start');
game.menu.interface = $('#main-interface');
game.menu.register = $('#register');
game.network = {};
game.var = {};

game.var.address = "https://localhost:44349/Player";

game.network.post = function(path, body, callback) {
    $.ajax({
        url:game.var.address + path,
        type: "POST",
        data: JSON.stringify(body),
        contentType:"application/json; charset=utf-8",
        dataType:"json",
        success: callback
    });
};

game.network.get = function(path, callback) {
    $.ajax({
        url:game.var.address + path,
        type:"GET",
        contentType:"application/json; charset=utf-8",
        dataType:"json",
        success: callback
    });
};

game.network.new = function(request) {
    game.network.post("/New", request, function(result) {
        game.var.id = result;
        game.menu.register.hide();
        game.menu.interface.show();
        setInterval(game.network.status, 1000);
    });
};

game.network.status = function() {
    game.network.get("/Status/" + game.var.id, game.menu.updateStatus);
};

game.menu.updateStatus = function(newMessages) {
    $.each(newMessages.messages, function(index, value) {
        game.menu.console.val(game.menu.console.val() + "\n" + value);
    });
};

game.network.move = function(direction) {
    body = {
        "Id": game.var.id,
        "Direction": direction
    };
    game.network.post("/Move", body, function(result) {

    });
};

game.network.up = () => game.network.move({"X": 0, "Y": -1 });
game.network.down = () => game.network.move({"X": 0, "Y": 1 });
game.network.left = () => game.network.move({"X": -1, "Y": 0 });
game.network.right = () => game.network.move({"X": 1, "Y": 0 });

$( document ).ready(function() {
    game.menu.start.click(function(e) {
        var request = {
            "Race": game.menu.race.val(),
            "Archetype": game.menu.archetype.val(),
            "Name": game.menu.name.val()
        };
        game.network.new(request);
    });
    game.menu.up.click(game.network.up);
    game.menu.down.click(game.network.down);
    game.menu.left.click(game.network.left);
    game.menu.right.click(game.network.right);
});