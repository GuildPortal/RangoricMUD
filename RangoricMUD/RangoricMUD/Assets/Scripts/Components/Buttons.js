function Buttons() {
    var vThis = this;
    vThis.Buttons = ko.observableArray();
}

Buttons.prototype = {
    Setup: function(tDependencies) {
        var vThis = this;
        //Account Management Main Buttons
        vThis.Buttons.push(new LoginButton(tDependencies));
        vThis.Buttons.push(new CreateAccountButton(tDependencies));
        vThis.Buttons.push(new ConfirmAccountButton(tDependencies));

        //Administration Main Buttons
        vThis.Buttons.push(new AdminButton(tDependencies));

        //Game List Stuff
        vThis.Buttons.push(new GameListButton(tDependencies));
    }
}