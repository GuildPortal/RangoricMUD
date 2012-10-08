function ViewModel(tSettings) {
    return {
        Page: ko.observable(tSettings.Page),
        Name: ko.observable(tSettings.Name),
        IsVisible: ko.computed(tSettings.IsVisible)
    };
}