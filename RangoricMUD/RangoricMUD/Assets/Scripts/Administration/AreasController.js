function AreasController(tDependencies) {
    return {
        List: function () {
            var vGameName = arguments[0][0];
            tDependencies.GameManager.CurrentGame(vGameName);
            var vThisResult = ViewModel({
                Page: "Areas/List",
                Name: vGameName,
                IsVisible: function() {
                    return tDependencies.AccountManager.IsAdmin();
                }
            });

            vThisResult.Areas = tDependencies.GameManager.CurrentGame().Areas;

            return vThisResult;
        }
    };
}