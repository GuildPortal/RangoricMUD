function AdminButton(tSettings) {
    var vThis = this;
    vThis.AccountManager = tSettings.AccountManager;

    vThis.IsVisible = ko.computed(function() {
        return vThis.AccountManager.IsAdmin();
    });
    vThis.IsEnabled = ko.computed(function() {
        return vThis.AccountManager.IsAdmin();
    });

    vThis.DisplayText = ko.observable("Admin");
}

AdminButton.prototype = {
    Click: function () {
        
    }
}