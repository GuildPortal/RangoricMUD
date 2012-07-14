function CreateAccountPage(tSettings) {
    var vThis = this;
    vThis.AccountManager = tSettings.AccountManager;
    vThis.Name = ko.observable("Create Account");
}

CreateAccountPage.prototype = {
    Submit: function (tForm) {
        
    }
};

ePages.CreateAccount = {};