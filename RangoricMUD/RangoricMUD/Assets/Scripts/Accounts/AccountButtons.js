function LoginButton(tSettings) {
    return Button({
        Text: "Login",
        IsVisible: function() {
            return !tSettings.AccountManager.IsLoggedIn();
        },
        IsEnabled: function() {
            return !tSettings.AccountManager.IsWorking();
        },
        Click: function() {
            tSettings.PageManager.GoToPage("Accounts", "Login");
        }
    });
}

function ConfirmAccountButton(tSettings) {
    return Button({
        Text: "Confirm Account",
        IsVisible: function() {
            var vFirst = tSettings.AccountManager.IsLoggedIn();
            var vSecond = !tSettings.AccountManager.IsConfirmed();

            return vFirst && vSecond;
        },
        IsEnabled: function() {
            return !tSettings.AccountManager.IsWorking();
        },
        Click: function() {
            tSettings.PageManager.GoToPage("Accounts", "Confirm");
        }
    });
}

function CreateAccountButton(tSettings) {
    return Button({
        Text: "Create Account",
        IsVisible: function() {
            return !tSettings.AccountManager.IsLoggedIn();
        },
        IsEnabled: function() {
            return !tSettings.AccountManager.IsWorking();
        },
        Click: function() {
            tSettings.PageManager.GoToPage("Accounts", "Create");
        }
    });
}