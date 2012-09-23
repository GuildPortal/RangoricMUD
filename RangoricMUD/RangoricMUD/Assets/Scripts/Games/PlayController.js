function PlayController(tDependencies) {
    return {
        Start: function () {
            var vGame = arguments[0][0];
            tDependencies.CharactersManager.LoadCharacters(vGame);
            return {
                Page: ko.observable("Play/Start"),
                IsVisible: ko.computed(function() {
                    return tDependencies.AccountManager.IsLoggedIn();
                }),
                Characters: tDependencies.CharactersManager.Characters,
                GoToCreateCharacter: function () {
                    tDependencies.PageManager.GoToPage("Characters", "Create", vGame);
                }
            };
        }
    };
}