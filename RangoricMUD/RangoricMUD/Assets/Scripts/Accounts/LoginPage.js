﻿function LoginPage(tSettings) {
    var vThis = this;
    vThis.AccountManager = tSettings.AccountManager;
    vThis.Name = ko.observable("Login Page");
}

LoginPage.prototype = {
    Submit: function(tForm) {
        var vThis = this;
        var vForm = $(tForm);
        if (vForm.valid()) {
            vThis.AccountManager.Login(vForm.serialize());
        }
    }
};

ePages.LoginPage = { };
    
