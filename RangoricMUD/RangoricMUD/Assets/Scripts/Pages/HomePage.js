function HomePage() {
    var vThis = this;
    vThis.Page = ko.observable(ePages.HomePage);
    vThis.IsVisible = ko.computed(function () {
        return true;
    });
    vThis.Buttons = ko.observableArray();
}