function LoginPage() {
    var vThis = this;
    vThis.Name = ko.observable("Login Page");
}

LoginPage.prototype = {
    Submit: function(tForm) {
        var vThis = this;
    }
};

ePages.LoginPage = {
    ViewModel: new LoginPage()
};
