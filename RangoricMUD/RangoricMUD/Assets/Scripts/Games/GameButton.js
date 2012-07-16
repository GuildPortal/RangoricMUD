function GameButton(tSettings) {
    var vThis = this;
    vThis.AccountManager = tSettings.AccountManager;
    vThis.PageManager = tSettings.PageManager;

    vThis.IsVisible = ko.computed(function () {
        return vThis.AccountManager.IsLoggedIn();
    });
    vThis.IsEnabled = ko.computed(function () {
        return vThis.AccountManager.IsLoggedIn();
    });

    vThis.DisplayText = ko.observable("Games");
}

GameButton.prototype = {
    Click: function () {
        var vThis = this;
        vThis.PageManager.ActivePage(ePages.GamePage);
    }
}