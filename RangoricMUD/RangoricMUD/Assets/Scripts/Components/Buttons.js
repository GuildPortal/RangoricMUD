function Button(tSettings) {
    return {
        Text: ko.observable(tSettings.Text),
        IsVisible: ko.computed(tSettings.IsVisible),
        IsEnabled: ko.computed(tSettings.IsEnabled),
        Click: tSettings.Click
    };
}

function Buttons() {
    var vThis = this;
    vThis.Buttons = ko.observableArray();
}

Buttons.prototype = {
    Setup: function(tDependencies) {
        var vThis = this;
        //Account Management Main Buttons
        vThis.Buttons.push(LoginButton(tDependencies));
        vThis.Buttons.push(CreateAccountButton(tDependencies));
        vThis.Buttons.push(ConfirmAccountButton(tDependencies));

        //Administration Main Buttons
        vThis.Buttons.push(AdminButton(tDependencies));

        //Game List Stuff
        vThis.Buttons.push(GameListButton(tDependencies));
    }
}