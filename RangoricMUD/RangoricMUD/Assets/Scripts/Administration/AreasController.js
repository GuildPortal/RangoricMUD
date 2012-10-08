function AreasController(tDependencies) {
    return {
        List: function () {
            var vThisResult = ViewModel({
                Page: "Areas/List",
                Name: "",
                IsVisible: function() {
                    return tDependencies.AccountManager.IsAdmin();
                }
            });

            return vThisResult;
        }
    };
}