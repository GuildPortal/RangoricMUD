function CharactersController(tDependencies) {
    return {
        Create: function () {
            var vGame = arguments[0];
            return {
                Page: ko.observable("Characters/Create"),
                IsVisible: ko.computed(function () {
                    return tDependencies.AccountManager.IsLoggedIn();
                })
            };
        },
        Select:function () {
            var vGame = arguments[0];
            return {
                Page: ko.observable("Characters/Select"),
                IsVisible: ko.computed(function() {
                    return tDependencies.AccountManager.IsLoggedIn();
                })
            };
        },
        View:function () {
            var vGame = arguments[0];
            var vCharacter = arguments[1];
            return {
                Page: ko.observable("Characters/View"),
                IsVisible: ko.computed(function() {
                    return tDependencies.AccountManager.IsLoggedIn();
                })
            };
        },
        EditRules: function () {
            var vGame = arguments[0];
            return {
                Page: ko.observable("Characters/EditRules"),
                IsVisible: ko.computed(function() {
                    return tDependencies.AccountManager.IsLoggedIn();
                })
            };
        }
    };
}