function AccountManager(tSettings) {
    var vThis = {
        IsLoggedIn: ko.observable(false),
        IsConfirmed: ko.observable(false),
        IsWorking: ko.observable(false),
        
        Name: ko.observable(null),
        Roles: ko.observableArray(),
        
        CheckLogin: function () {
            vHub.checkLogin();
        },
        Login: function (tData) {
            vHub.login(tData);
        },
        CreateAccount: function (tData) {
            vHub.createAccount(tData);
        },
        ConfirmAccount: function (tData) {
            vHub.confirmAccount(tData);
        }
    };

    vThis.IsPlayer = ko.computed(function() {
        return vThis.Roles.indexOf(2) >= 0;
    });
    vThis.IsAdmin = ko.computed(function() {
        return vThis.Roles.indexOf(1) >= 0;
    });
    
    var vHub = tSettings.Connection.accountHub;
    vHub.AddAccount = function (tData) {
        if (tData) {
            vThis.IsLoggedIn(tData.IsLoggedIn);
            vThis.IsConfirmed(tData.IsConfirmed);
            vThis.Name(tData.Name);
            vThis.Roles.removeAll();
            for (var vIndex = 0; vIndex < tData.Roles.length; vIndex++) {
                vThis.Roles.push(tData.Roles[vIndex]);
            }
        }
    };
    vHub.AddConfirmation = function(tData) {
        vThis.IsConfirmed(tData);
    };
    
    return vThis;
}