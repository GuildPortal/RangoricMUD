ePages.GamePage = {};

function GamePage(tSettings) {
    var vThis = this;
    vThis.AccountManager = tSettings.AccountManager;
    vThis.GameManager = tSettings.GameManager;

    vThis.Page = ko.observable(ePages.GamePage);
    vThis.IsVisible = ko.computed(function () {
        return vThis.AccountManager.IsLoggedIn();
    });
    vThis.AccountManager.IsLoggedIn.subscribe(function(tData) {
        if(tData) {
            vThis.GameManager.GetGames();
        }
    });

    vThis.CreateGameName = ko.observable("");
}

GamePage.prototype = {
    CreateGameSubmit: function () {
        var vThis = this;
        var vData = {
            Name: vThis.CreateGameName()
        };
        vThis.GameManager.CreateGame(vData);
    }
};

