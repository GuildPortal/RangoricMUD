function PageManager() {
    var vThis = this;
    vThis.PageElement = $(".Page");
    vThis.Page = ko.observable();
    vThis.ActivePage = ko.computed({
        read: function () {
            return vThis.Page();
        },
        write: function (tPage) {
            if(vThis.PageStillVisible) {
                vThis.PageStillVisible.dispose();
            }
            vThis.Page(tPage.ViewModel);
            vThis.PageStillVisible = tPage.ViewModel.IsVisible.subscribe(function(tData) {
                if(!tData) {
                    vThis.ActivePage(ePages.HomePage);
                }
            });
            jQuery.validator.unobtrusive.parse(vThis.PageElement);
        },
        owner: vThis
    });
}

PageManager.prototype = {
    Start: function () {
        var vThis = this;
        vThis.ActivePage(ePages.HomePage);
    }
}