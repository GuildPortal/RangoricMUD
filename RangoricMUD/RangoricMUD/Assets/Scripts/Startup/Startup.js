/// <reference path="~/Assets/Scripts/Components/Ajax.js"/>
/// <reference path="~/Assets/Scripts/Components/Buttons.js"/>
/// <reference path="~/Assets/Scripts/Components/Controllers.js"/>
/// <reference path="~/Assets/Scripts/Accounts/AccountManager.js"/>
/// <reference path="~/Assets/Scripts/Administration/AdminManager.js"/>
/// <reference path="~/Assets/Scripts/Games/GameManager.js"/>

$(function() {
    var vDependencies = {
        AjaxFactory: Ajax,
        Connection: $.connection
    };
    vDependencies.PageManager = new PageManager(vDependencies);
    vDependencies.AccountManager = new AccountManager(vDependencies);
    vDependencies.GameManager = GameManager(vDependencies);
    vDependencies.AdminManager = new AdminManager(vDependencies);
    vDependencies.CharactersManager = CharactersManager(vDependencies);
    vDependencies.Controllers = Controllers(vDependencies);

    var vButtons = Buttons(vDependencies);

    vDependencies.AccountManager.Start();
    vDependencies.PageManager.Start();

    $.connection.hub.start();

    ko.applyBindings(vButtons, $('#Main-Menu').get()[0]);
    ko.applyBindings(vDependencies.PageManager.Page, $('.Page').get()[0]);
});