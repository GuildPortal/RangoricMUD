function ConfirmAccountButton(tSettings) {
    var vThis = this;
    vThis.AccountManager = tSettings.AccountManager;
    vThis.PageManager = tSettings.PageManager;

    vThis.IsVisible = ko.computed(function () {
        var vFirst = vThis.AccountManager.IsLoggedIn();
        var vSecond = !vThis.AccountManager.IsConfirmed();

        return vFirst && vSecond;
    });
    vThis.IsEnabled = ko.computed(function () {
        return !vThis.AccountManager.IsWorking();
    });

    vThis.DisplayText = ko.observable("Confirm Account");
}

ConfirmAccountButton.prototype = {
    Click: function () {
        var vThis = this;
        vThis.PageManager.GoToPage("Accounts", "Confirm");
    }
}