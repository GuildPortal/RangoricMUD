function CreateAccountPage(tSettings) {
    var vThis = this;
    vThis.AccountManager = tSettings.AccountManager;
    vThis.Page = ko.observable(ePages.CreateAccount);
    vThis.IsVisible = ko.computed(function() {
        return !vThis.AccountManager.IsLoggedIn();
    });

    vThis.Name = ko.observable("");
    vThis.Password = ko.observable("");
    vThis.Email = ko.observable("");
    vThis.Buttons = ko.observableArray();
}

CreateAccountPage.prototype = {
    Submit: function (tForm) {
        var vThis = this;
        var vForm = $(tForm);
        if(vForm.valid()) {
            vThis.AccountManager.CreateAccount({ Name: vThis.Name(), Password: vThis.Password(), Email: vThis.Email() });
        }
    }
};
