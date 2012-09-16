function CharactersController(tDependencies) {
    return {
        Create: function () {
            var vGame = arguments[0][0];
            return {
                Page: ko.observable("Characters/Create"),
                IsVisible: ko.computed(function () {
                    return tDependencies.AccountManager.IsLoggedIn();
                }),
                ListName: ko.observable(""),
                Name: ko.observable(""),
                Submit: function (tForm) {
                    var vThisAction = this;
                    var vForm = $(tForm);
                    if(vForm.valid()) {
                        tDependencies.CharactersManager.Create({ Name: vThisAction.Name(), ListName: vThisAction.ListName(), GameName: vGame });
                    }
                }
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
            var vGame = arguments[0][0];
            var vCharacter = arguments[0][1];
            return {
                Page: ko.observable("Characters/View"),
                IsVisible: ko.computed(function() {
                    return tDependencies.AccountManager.IsLoggedIn();
                })
            };
        },
        EditRules: function () {
            var vGame = arguments[0][0];
            return {
                Page: ko.observable("Characters/EditRules"),
                IsVisible: ko.computed(function() {
                    return tDependencies.AccountManager.IsLoggedIn();
                })
            };
        }
    };
}