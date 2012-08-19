function AccountsController(tDependencies) {
    return {
        Login: function() {
            return {
                Page: ko.observable("Accounts/Login"),
                IsVisible: ko.computed(function() {
                    return !tDependencies.AccountManager.IsLoggedIn();
                }),
                Name: ko.observable(""),
                Password: ko.observable(""),
                Submit: function(tForm) {
                    var vThisAction = this;
                    var vForm = $(tForm);
                    if (vForm.valid()) {
                        tDependencies.AccountManager.Login({ Name: vThisAction.Name(), Password: vThisAction.Password() });
                    }
                }
            };
        },
        Create:function () {
            return {
                Page: ko.observable("Accounts/Create"),
                IsVisible: ko.computed(function() {
                    return !tDependencies.AccountManager.IsLoggedIn();
                }),
                Name: ko.observable(""),
                Password: ko.observable(""),
                Email: ko.observable(""),
                Submit: function(tForm) {
                    var vThisAction = this;
                    var vForm = $(tForm);
                    if (vForm.valid()) {
                        tDependencies.AccountManager.CreateAccount({ Name: vThisAction.Name(), Password: vThisAction.Password(), Email: vThisAction.Email() });
                    }
                }
            };
        },
        Confirm: function () {
            return {
                Page: ko.observable("Accounts/Confirm"),
                IsVisible: ko.computed(function() {
                    return !tDependencies.AccountManager.IsConfirmed();
                }),
                Name: ko.observable(""),
                Password: ko.observable(""),
                ConfirmationNumber: ko.observable(""),
                Submit: function (tForm) {
                    var vThisAction = this;
                    var vForm = $(tForm);

                    if (vForm.valid()) {
                        tDependencies.AccountManager.ConfirmAccount({ Name: vThisAction.Name(), Password: vThisAction.Password(), ConfirmationNumber: vThisAction.ConfirmationNumber() });
                    }
                }
            };
        }
    };
}