var ePages = { };
ePages.HomePage = { };

function HomePage() {
    var vThis = this;
    vThis.Page = ko.observable(ePages.HomePage);
    vThis.IsVisible = ko.computed(function () {
        return true;
    });
    vThis.Buttons = ko.observableArray();
}

ePages.HomePage.ViewModel = new HomePage();

function PageManager() {
    var vThis = this;
    vThis.Page = ko.observable(ePages.HomePage.ViewModel);
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
            jQuery.validator.unobtrusive.parse($(".Page"));
        },
        owner: vThis
    });
}

PageManager.prototype = {
    Start: function () {
        
    }
}