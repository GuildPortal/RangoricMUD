function GameListButton(tSettings) {
    return Button({
        Text: "Games",
        IsVisible: function() {
            return tSettings.AccountManager.IsLoggedIn();
        },
        IsEnabled: function() {
            return tSettings.AccountManager.IsLoggedIn();
        },
        Click: function() {
            tSettings.PageManager.GoToPage("Games", "List");
        }
    });
}