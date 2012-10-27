
function GameManager(tSettings) {
    var vCurrentGame = ko.observable(null);
    var vThis = {
        GameList: ko.observableArray(),
        CreateGame: function (tCreateGameModel) {
            vHub.createGame(tCreateGameModel);
        }
    };
    vThis.CurrentGame = ko.computed({
        read: function() {
            return vCurrentGame();
        },
        write: function(tName) {
            for (var vIndex = 0; vIndex < vThis.GameList().length; vIndex++) {
                var vGame = vThis.GameList()[vIndex];
                if(vGame.Name() === tName) {
                    vCurrentGame(vGame);
                }
            }
        }
    });

    var vHub = tSettings.Connection.gameHub;
    vHub.AddGame = function (tData) {
        if (vThis.GameList.indexOf(tData) >= 0) {
            vThis.GameList.remove(tData);
        }
        vThis.GameList.push(Game(tData));
    };
    vHub.AddGames = function(tList) {
        vThis.GameList.removeAll();
        for (var vIndex = 0; vIndex < tList.length; vIndex++) {
            var vItem = tList[vIndex];
            vThis.GameList.push(Game(vItem));
        }
    };
    
    tSettings.AccountManager.IsLoggedIn.subscribe(function(tData) {
        if (tData) {
            vHub.getAll();
        }
    });
    return vThis;
}