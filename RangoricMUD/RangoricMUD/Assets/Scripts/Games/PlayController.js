function PlayController(tDependencies) {
    return {
        Start: function () {
            var vGame = arguments[0];
            return {
                Page: ko.observable("Play/Start"),
                IsVisible: ko.computed(function() {
                    return tDependencies.AccountManager.IsLoggedIn();
                }),
                Characters: ko.observableArray(),
                GoToCreateCharacter: function () {
                    tDependencies.PageManager.GoToPage("Characters", "Create", vGame);
                }
            };
        }
    };
}