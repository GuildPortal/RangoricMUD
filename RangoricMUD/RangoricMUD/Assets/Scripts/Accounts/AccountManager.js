/// <reference path="../Libraries/knockout.js"/>
/// <reference path="../Settings/Urls.js"/>
function AccountManager(tSettings) {
    var vThis = this;

    vThis.AjaxFactory = tSettings.AjaxFactory;
    
    vThis.IsLoggedIn = ko.observable(false);
    vThis.Name = ko.observable(null);
    vThis.Roles = ko.observableArray();
    vThis.IsPlayer = ko.computed(function() {
        return vThis.Roles.indexOf(2) >= 0;
    });
    vThis.IsAdmin = ko.computed(function() {
        return vThis.Roles.indexOf(1) >= 0;
    });
    
    vThis.IsWorking = ko.observable(false);
}
AccountManager.prototype = {
    Start: function () {
        var vThis = this;
        vThis.CheckLogin();
    },
    CheckLogin: function () {
        var vThis = this;
        vThis.IsWorking(true);
        vThis.Ajax = new vThis.AjaxFactory();

        vThis.Ajax.Settings
            .Url(vUrls.CheckLogin)
            .IsPost()
            .IsJson()
            .Success(function(tData) {
                vThis.IsLoggedIn(tData.IsLoggedIn);
                vThis.Name(tData.Name);
                for (var vIndex = 0; vIndex < tData.Roles.length;vIndex++) {
                    vThis.Roles.push(tData.Roles[vIndex]);
                }
                vThis.IsWorking(false);
            });
        vThis.Ajax.Start();
    },
    Login: function (tLoginData) {
        var vThis = this;
        if(!vThis.IsWorking()) {
            vThis.IsWorking(true);
            vThis.Ajax = new vThis.AjaxFactory();
            vThis.Ajax.Settings
                .Url(vUrls.Login)
                .UsingData(tLoginData)
                .IsPost()
                .IsJson()
                .Success(function (tData) {
                    vThis.IsWorking(false);
                    vThis.CheckLogin();
                });
            vThis.Ajax.Start();
        }
    },
    CreateAccount: function (tCreateAccountData) {
        var vThis = this;
        if(!vThis.IsWorking()) {
            vThis.IsWorking(true);
            vThis.Ajax = new vThis.AjaxFactory();
            vThis.Ajax.Settings
                .Url(vUrls.CreateAccount)
                .UsingData(tCreateAccountData)
                .IsPost()
                .IsJson()
                .Success(function (tData) {
                    vThis.IsWorking(false);
                    if (tData) {
                        vThis.Login(tCreateAccountData);
                    }
                });
            vThis.Ajax.Start();
        }
    }
}