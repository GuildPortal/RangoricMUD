function AdminButton(tSettings) {
    return Button({
        Text: "Admin",
        IsVisible: function () {
            return tSettings.AccountManager.IsAdmin();
        },
        IsEnabled: function () {
            return tSettings.AccountManager.IsAdmin();
        },
        Click: function () {
            
        }
    });
}