﻿/// <reference path="../Libraries/knockout.js"/>
/// <reference path="../Settings/Urls.js"/>
function AccountManager(tSettings) {
    var vThis = this;

    vThis.Connection = tSettings.Connection;
    
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
        vThis.Hub = vThis.Connection.accountHub;
    },
    CheckLogin: function () {
        var vThis = this;
        vThis.Hub.checkLogin()
            .done(function (tData) {
                vThis.IsLoggedIn(tData.IsLoggedIn);
                vThis.Name(tData.Name);
                vThis.Roles.removeAll();
                for (var vIndex = 0; vIndex < tData.Roles.length; vIndex++) {
                    vThis.Roles.push(tData.Roles[vIndex]);
                }
            });
    },
    Login: function (tLoginData) {
        var vThis = this;
        vThis.Hub
            .login(tLoginData)
            .done(function(tData) {
                if(tData) {
                    vThis.CheckLogin();
                }
            });
    },
    CreateAccount: function (tCreateAccountData) {
        var vThis = this;
        vThis.Hub.createAccount(tCreateAccountData)
            .done(function (tResult) {
                if(tResult) {
                    var vData = {
                        Name: tCreateAccountData.Name,
                        Password: tCreateAccountData.Password
                    };
                    vThis.Login(vData);
                }
            });
    }
}