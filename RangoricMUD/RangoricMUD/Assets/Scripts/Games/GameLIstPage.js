function GameListPage(tSettings) {
    var vThis = this;
    vThis.AccountManager = tSettings.AccountManager;
    vThis.GameManager = tSettings.GameManager;
    vThis.PageManager = tSettings.PageManager;

    vThis.Page = ko.observable(ePages.GameListPage);
    vThis.IsVisible = ko.computed(function () {
        return vThis.AccountManager.IsLoggedIn();
    });
    vThis.AccountManager.IsLoggedIn.subscribe(function(tData) {
        if(tData) {
            vThis.GameManager.GetGames();
        }
    });

    vThis.CreateGameName = ko.observable("");

    vThis.GameList = vThis.GameManager.GameList;
    vThis.Buttons = ko.observableArray();
}

GameListPage.prototype = {
    SubmitCreateGame: function (tForm) {
        var vThis = this;
        var vForm = $(tForm);
        if (vForm.valid()) {
            var vData = {
                Name: vThis.CreateGameName()
            };
            vThis.GameManager.CreateGame(vData);
        }
    },
    GoEditGame:function(tName) {
        var vThis = this;
        return function() {
            vThis.PageManager.ActivePage(ePages.GameEditPage);
            ePages.GameEditPage.ViewModel.StartEditing(tName);
        };
    }
};

