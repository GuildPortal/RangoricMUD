function LoginButton(tSettings) {
    var vThis = this;
    vThis.AccountManager = tSettings.AccountManager;
    vThis.PageManager = tSettings.PageManager;

    vThis.IsVisible = ko.computed(function() {
        return !vThis.AccountManager.IsLoggedIn();
    });
    vThis.IsEnabled = ko.computed(function() {
        return !vThis.AccountManager.IsWorking();
    });

    vThis.DisplayText = ko.observable("Login");
}

LoginButton.prototype = {
    Click: function () {
        var vThis = this;
        vThis.PageManager.ActivePage(ePages.LoginPage);
    }
}