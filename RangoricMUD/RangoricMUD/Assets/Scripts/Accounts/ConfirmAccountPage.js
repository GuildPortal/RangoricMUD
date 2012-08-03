ePages.ConfirmAccountPage = {};

function ConfirmAccountPage(tSettings) {
    var vThis = this;
    vThis.AccountManager = tSettings.AccountManager;
    vThis.Page = ko.observable(ePages.ConfirmAccountPage);
    vThis.IsVisible = ko.computed(function() {
        return !vThis.AccountManager.IsConfirmed();
    });

    vThis.Name = ko.observable("");
    vThis.Password = ko.observable("");
    vThis.ConfirmationNumber = ko.observable("");
    vThis.Buttons = ko.observableArray();
}

ConfirmAccountPage.prototype = {
    Submit: function (tForm) {
        var vThis = this;
        var vForm = $(tForm);
        
        if(vForm.valid()) {
            vThis.AccountManager.ConfirmAccount({ Name: vThis.Name(), Password: vThis.Password(), ConfirmationNumber: vThis.ConfirmationNumber() });
        }
    }
}