function GamesController(tDependencies) {
    return {
        List: function() {
            return {
                Page: ko.observable("Games/List"),
                IsVisible: ko.computed(function() {
                    return tDependencies.AccountManager.IsLoggedIn();
                }),
                Name: ko.observable(""),
                List: tDependencies.GameManager.GameList,
                SubmitCreateGame: function(tForm) {
                    var vThisAction = this;
                    var vForm = $(tForm);
                    if (vForm.valid()) {
                        var vData = {
                            Name: vThisAction.Name()
                        };
                        tDependencies.GameManager.CreateGame(vData);
                    }
                },
                GoEdit: function(tName) {
                    return function() {
                        tDependencies.PageManager.GoToPage("Games", "Edit", tName);
                    };
                },
                GoPlay: function (tName) {
                    return function() {
                        tDependencies.PageManager.GoToPage("Play", "Start", tName);
                    };
                }
            };
        },
        Edit:function () {
            var vName = arguments[0];

            return {
                Page: ko.observable("Games/Edit"),
                IsVisible: ko.computed(function() {
                    return tDependencies.AccountManager.IsLoggedIn();
                }),
                Name: ko.observable(vName)
            };
        }
    };
}