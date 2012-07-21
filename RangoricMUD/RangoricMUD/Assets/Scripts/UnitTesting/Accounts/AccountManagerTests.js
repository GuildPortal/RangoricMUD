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