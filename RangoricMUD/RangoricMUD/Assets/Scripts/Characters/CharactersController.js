function CharactersController(tDependencies) {
    return {
        Create: function () {
            var vGame = arguments[0];
            return {
                Page: "Characters/Create",
                IsVisible: ko.computed(function () {
                    return tDependencies.AccountManager.IsLoggedIn();
                })
            };
        },
        Select:function () {
            var vGame = arguments[0];
            return {
                Page: "Characters/Select",
                IsVisible: ko.computed(function() {
                    return tDependencies.AccountManager.IsLoggedIn();
                })
            };
        },
        View:function () {
            var vGame = arguments[0];
            var vCharacter = arguments[1];
            return {
                Page: "Characters/View",
                IsVisible: ko.computed(function() {
                    return tDependencies.AccountManager.IsLoggedIn();
                })
            };
        }
    };
}