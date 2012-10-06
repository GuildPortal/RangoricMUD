function GameManager(tSettings) {
    var vHub = tSettings.Connection.gameHub;
    var vThis = {
        GameList: ko.observableArray(),
        CurrentGame: ko.observable(""),
        CreateGame: function (tCreateGameModel) {
            vHub.createGame(tCreateGameModel);
        }
    };

    vHub.AddGame = function(tData) {
        if (vThis.GameList.indexOf(tData) >= 0) {
            vThis.GameList.remove(tData);
        }
        vThis.GameList.push(tData);
    };
    vHub.AddGames = function(tList) {
        vThis.GameList.removeAll();
        for (var vIndex = 0; vIndex < tList.length; vIndex++) {
            var vItem = tList[vIndex];
            vThis.GameList.push(vItem);
        }
    };
    
    vThis.Connection = tSettings.Connection;

    tSettings.AccountManager.IsLoggedIn.subscribe(function(tData) {
        if (tData) {
            vHub.getAll();
        }
    });
    return vThis;
}