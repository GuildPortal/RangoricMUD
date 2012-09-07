/// <reference path="~/Assets/Scripts/Libraries/jquery.js"/>
/// <reference path="~/Assets/Scripts/Libraries/knockout.js"/>
/// <reference path="~/Assets/Scripts/Components/Ajax.js"/>
/// <reference path="~/Assets/Scripts/Components/Buttons.js"/>
/// <reference path="~/Assets/Scripts/Components/Controllers.js"/>
/// <reference path="~/Assets/Scripts/Accounts/AccountManager.js"/>
/// <reference path="~/Assets/Scripts/Accounts/LoginButton.js"/>
/// <reference path="~/Assets/Scripts/Accounts/CreateAccountButton.js"/>
/// <reference path="~/Assets/Scripts/Accounts/ConfirmAccountButton.js"/>
/// <reference path="~/Assets/Scripts/Administration/AdminManager.js"/>
/// <reference path="~/Assets/Scripts/Administration/AdminButton.js"/>
/// <reference path="~/Assets/Scripts/Games/GameManager.js"/>
/// <reference path="~/Assets/Scripts/Games/GameListButton.js"/>

$(function () {
    var vDependencies = {
        AjaxFactory: Ajax,
        Connection: $.connection
    };
    vDependencies.PageManager = new PageManager(vDependencies);
    vDependencies.AccountManager = new AccountManager(vDependencies);
    vDependencies.GameManager = new GameManager(vDependencies);
    vDependencies.AdminManager = new AdminManager(vDependencies);
    vDependencies.Controllers = Controllers(vDependencies);
    
    var vButtons = new Buttons();
    vButtons.Setup(vDependencies);
    
    vDependencies.AccountManager.Start();
    vDependencies.GameManager.Start();
    vDependencies.PageManager.Start();
    
    $.connection.hub.start();

    ko.applyBindings(vButtons, $('#Main-Menu').get()[0]);
    ko.applyBindings(vDependencies.PageManager.Page, $('.Page').get()[0]);
})