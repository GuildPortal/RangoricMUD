/// <reference path="~/Assets/Scripts/Libraries/jquery.js"/>
/// <reference path="~/Assets/Scripts/Libraries/knockout.js"/>
/// <reference path="~/Assets/Scripts/Components/Button.js"/>
/// <reference path="~/Assets/Scripts/Components/PageManager.js"/>
/// <reference path="~/Assets/Scripts/Accounts/AccountManager.js"/>
/// <reference path="~/Assets/Scripts/Accounts/LoginPage.js"/>
/// <reference path="~/Assets/Scripts/Accounts/LoginButton.js"/>
/// <reference path="~/Assets/Scripts/Accounts/CreateAccountButton.js"/>
/// <reference path="~/Assets/Scripts/Accounts/CreateAccountPage.js"/>
/// <reference path="~/Assets/Scripts/Administration/AdminButton.js"/>

$(function () {
    var vDependencies = {
        AjaxFactory: Ajax
    };
    vDependencies.AccountManager = new AccountManager(vDependencies);
    vDependencies.AccountManager.Start();

    vDependencies.PageManager =  new PageManager();
    ePages.LoginPage.ViewModel = new LoginPage(vDependencies);
    ePages.CreateAccount.ViewModel = new CreateAccountPage(vDependencies);

    var vButtons = {};
    vButtons.Buttons = ko.observableArray();
    
    //Account Management Main Buttons
    vButtons.Buttons.push(new LoginButton(vDependencies));
    vButtons.Buttons.push(new CreateAccountButton(vDependencies));
    
    //Administration Main Buttons
    vButtons.Buttons.push(new AdminButton(vDependencies));

    ko.applyBindings(vButtons, $('#Main-Menu').get()[0]);
    ko.applyBindings(vDependencies.PageManager.Page, $('.Page').get()[0]);
})