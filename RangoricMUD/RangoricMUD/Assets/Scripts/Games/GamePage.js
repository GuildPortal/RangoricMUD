ePages.GamePage = {};

function GamePage(tSettings) {
    var vThis = this;
    vThis.AccountManager = tSettings.AccountManager;
    vThis.GameManager = tSettings.GameManager;

    vThis.Page = ko.observable(ePages.GamePage);
    vThis.IsVisible = ko.computed(function () {
        return vThis.AccountManager.IsLoggedIn();
    });
}

GamePage.prototype = {
    
};

