function HomePage() {
    var vThis = this;
    vThis.Name = ko.observable("Home Page");
}
var ePages = {
    HomePage: {
        ViewModel: new HomePage()
    }
};

function PageManager() {
    var vThis = this;
    vThis.Page = ko.observable(ePages.HomePage.ViewModel);
    vThis.ActivePage = ko.computed({
        read: function () {
            return vThis.Page();
        },
        write: function (tPage) {
            vThis.Page(tPage.ViewModel);
        },
        owner: vThis
    });
}

PageManager.prototype = {
    Start: function () {
        
    }
}