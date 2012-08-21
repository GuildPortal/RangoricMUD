function PlayController(tDependencies) {
    return {
        Start: function () {
            var vGame = arguments[0];
            return {
                Page: "Play/Start",
                IsVisible: ko.computer(function() {
                    return tDependencies.AccountManager.IsLoggedIn();
                })
            };
        }  
    };
}