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
    vDependencies.AccountManager = AccountManager(vDependencies);
    vDependencies.GameManager = GameManager(vDependencies);
    vDependencies.AdminManager = AdminManager(vDependencies);
    vDependencies.CharactersManager = CharactersManager(vDependencies);
    vDependencies.Controllers = Controllers(vDependencies);

    var vButtons = Buttons(vDependencies);


    $.connection.hub.start(function () {
        vDependencies.PageManager.Start();
        ko.applyBindings(vButtons, $('#Main-Menu').get()[0]);
        ko.applyBindings(vDependencies.PageManager.Page, $('.Page').get()[0]);
    });
});