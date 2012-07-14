/// <reference path="~/Assets/Scripts/Libraries/jquery.js"/>
/// <reference path="~/Assets/Scripts/Libraries/knockout.js"/>
/// <reference path="~/Assets/Scripts/Components/Button.js"/>
/// <reference path="~/Assets/Scripts/Components/PageManager.js"/>
/// <reference path="~/Assets/Scripts/Accounts/AccountManager.js"/>
/// <reference path="~/Assets/Scripts/Accounts/LoginPage.js"/>
/// <reference path="~/Assets/Scripts/Accounts/LoginButton.js"/>
/// <reference path="~/Assets/Scripts/Accounts/CreateAccountButton.js"/>
/// <reference path="~/Assets/Scripts/Administration/AdminButton.js"/>

$(function () {
    var vAccountManager = new AccountManager({ AjaxFactory: Ajax });
    vAccountManager.Start();

    var vPageManager = new PageManager();

    var vButtons = {};
    vButtons.Buttons = ko.observableArray();
    
    //Account Management Main Buttons
    vButtons.Buttons.push(new LoginButton({ AccountManager: vAccountManager, PageManager: vPageManager }));
    vButtons.Buttons.push(new CreateAccountButton({ AccountManager: vAccountManager, PageManager: vPageManager }));
    
    //Administration Main Buttons
    vButtons.Buttons.push(new AdminButton({ AccountManager: vAccountManager }));

    ko.applyBindings(vButtons, $('#Main-Menu').get()[0]);
    ko.applyBindings(vPageManager.Page, $('.Page').get()[0]);
})