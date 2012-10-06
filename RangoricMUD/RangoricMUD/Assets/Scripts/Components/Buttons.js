function Button(tSettings) {
    return {
        Text: ko.observable(tSettings.Text),
        IsVisible: ko.computed(tSettings.IsVisible),
        IsEnabled: ko.computed(tSettings.IsEnabled),
        Click: tSettings.Click
    };
}

function Buttons(tDependencies) {
    var vThis = {
        Buttons: ko.observableArray([
            //Account Management Main Buttons
            LoginButton(tDependencies),
            CreateAccountButton(tDependencies),
            ConfirmAccountButton(tDependencies),
            //Administration Main Buttons
            AdminButton(tDependencies),
            //Game List Stuff
            GameListButton(tDependencies)
        ]),
    };
    return vThis;
}