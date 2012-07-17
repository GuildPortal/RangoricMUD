ePages.LoginPage = {};

function LoginPage(tSettings) {
    var vThis = this;
    vThis.AccountManager = tSettings.AccountManager;
    vThis.Page = ko.observable(ePages.LoginPage);
    vThis.IsVisible = ko.computed(function () {
        return !vThis.AccountManager.IsLoggedIn();
    });

    vThis.Name = ko.observable("");
    vThis.Password = ko.observable("");
}

LoginPage.prototype = {
    Submit: function(tForm) {
        var vThis = this;
        var vForm = $(tForm);
        if (vForm.valid()) {
            vThis.AccountManager.Login({Name: vThis.Name(), Password: vThis.Password()});
        }
    }
};

    
