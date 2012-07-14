function LoginPage(tSettings) {
    var vThis = this;
    vThis.AccountManager = tSettings.AccountManager;
    vThis.Name = ko.observable("Login Page");
}

LoginPage.prototype = {
    Submit: function(tForm) {
        var vThis = this;
    }
};

ePages.LoginPage = { };
    
