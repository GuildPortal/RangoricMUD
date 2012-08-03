function GameManager(tSettings) {
    var vThis = this;
    vThis.Connection = tSettings.Connection;

    vThis.GameList = ko.observableArray();
}

GameManager.prototype = {
    Start: function () {
        var vThis = this;
        vThis.Hub = vThis.Connection.gameHub;
    },
    CreateGame: function (tCreateGameModel) {
        var vThis = this;
        vThis.Hub
            .createGame(tCreateGameModel)
            .done(function(tData) {
                if(tData === 0) {
                    vThis.GetGame(tCreateGameModel.Name);
                }
            });
    },
    GetGame: function (tName) {
        var vThis = this;
        vThis.Hub
            .getGame({ Name: tName })
            .done(function(tGame) {
                if(vThis.GameList.indexOf(tGame) >= 0) {
                    vThis.GameList.remove(tGame);
                }
                vThis.AddGame(tGame);
            });
    },
    GetGames: function () {
        var vThis = this;
        vThis.Hub
            .getAll()
            .done(function (tList) {
                vThis.GameList.removeAll();
                for (var vIndex = 0; vIndex < tList.length; vIndex++) {
                    var vItem = tList[vIndex];
                    vThis.AddGame(vItem);
                }
            });
    },
    AddGame:function (tGame) {
        var vThis = this;
        vThis.GameList.push(tGame);
    }
}