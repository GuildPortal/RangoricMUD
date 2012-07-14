function CreateAccountPage(tSettings) {
    var vThis = this;
    vThis.AccountManager = tSettings.AccountManager;
    vThis.Name = ko.observable("Create Account");
}

CreateAccountPage.prototype = {
    Submit: function (tForm) {
        var vThis = this;
        var vForm = $(tForm);
        if(vForm.valid()) {
            vThis.AccountManager.CreateAccount(vForm.serialize());
        }
    }
};

ePages.CreateAccount = {};