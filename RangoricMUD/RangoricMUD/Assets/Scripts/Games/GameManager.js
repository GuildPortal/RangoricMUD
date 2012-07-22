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
                    //Creation Successful
                }
            });
    },
    GetGames: function () {
        var vThis = this;
        vThis.Hub
            .getAll()
            .done(function (tList) {
                vThis.GameList.removeAll();
                for(var vIndex = 0;vIndex < tList.length;vIndex++) {
                    vThis.GameList.push(tList[vIndex]);
                }
            });
    }
}