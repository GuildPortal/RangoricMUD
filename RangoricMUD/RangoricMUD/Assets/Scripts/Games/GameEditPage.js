function GameEditPage(tDependencies) {
    var vThis = this;
    vThis.AccountManager = tDependencies.AccountManager;
    vThis.Page = ko.observable(ePages.GameEditPage);
    vThis.IsVisible = ko.computed(function () {
        return vThis.AccountManager.IsLoggedIn();
    });
    vThis.Buttons = ko.observableArray();
    vThis.GameName = ko.observable("");
}
GameEditPage.prototype = {
    StartEditing: function (tGameName) {
        var vThis = this;
        vThis.GameName(tGameName);
    }
}