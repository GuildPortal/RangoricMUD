/// <reference path="../../Accounts/AccountManager.js"/>
/// <reference path="../../Components/Ajax.js"/>
/// <reference path="../qunit.js"/>

function FakeSuccessAjax() {
    var vThis = this;
    vThis.Settings = new AjaxSettings();
}

FakeSuccessAjax.prototype = {
    Start: function() {
        var vThis = this;
        vThis.Settings.success(vThis.SuccessData);
        if(vThis.OnSuccess) {
            vThis.OnSuccess();
        }
    }
};

test("The Constructor Works for AccountManager", function() {
    var vAccountManager = new AccountManager({AjaxFactory: FakeSuccessAjax});
});

test("A logged in user sets is logged in to true when you hit start", 3, function () {
    FakeSuccessAjax.prototype.OnSuccess = function() {
        equal(true, vAccountManager.IsLoggedIn());
        equal(true, vAccountManager.IsAdmin());
        equal("Test Account", vAccountManager.Name());
    };
    FakeSuccessAjax.prototype.SuccessData = {
          Value: {
              IsLoggedIn: true,
              Name: "Test Account",
              Roles:[1]
          }
    };
    var vAccountManager = new AccountManager({ AjaxFactory: FakeSuccessAjax });
    vAccountManager.Start();
});

test("IsWorking is cleared after getting logged in status", 1, function() {
    FakeSuccessAjax.prototype.OnSuccess = function() {
        equal(false, vAccountManager.IsWorking());
    };
    FakeSuccessAjax.prototype.SuccessData = {
        Value: {
            IsLoggedIn: true,
            Name: "Test Account",
            Roles: [1]
        }
    };
    var vAccountManager = new AccountManager({ AjaxFactory: FakeSuccessAjax });
    vAccountManager.Start();
});

test("Login does a check login when it works to get the account info", 2, function() {
    
    FakeSuccessAjax.prototype.OnSuccess = function () {
        equal(true, vAccountManager.IsLoggedIn());
    };
    FakeSuccessAjax.prototype.SuccessData = {
        Value: {
            IsLoggedIn: true,
            Name: "Test Account",
            Roles: [1]
        }
    };
    var vAccountManager = new AccountManager({ AjaxFactory: FakeSuccessAjax });
    vAccountManager.Login({ Name: "Test Account", Password: "Password" });
});

test("CreateAccount does a login, then checks the login.", 3, function () {

    FakeSuccessAjax.prototype.OnSuccess = function () {
        equal(true, vAccountManager.IsLoggedIn());
    };
    FakeSuccessAjax.prototype.SuccessData = {
        Value: {
            IsLoggedIn: true,
            Name: "Test Account",
            Roles: [1]
        }
    };
    var vAccountManager = new AccountManager({ AjaxFactory: FakeSuccessAjax });
    vAccountManager.CreateAccount({ Name: "Test Account", Password: "Password", Email: "Email@Email.Email" });
});