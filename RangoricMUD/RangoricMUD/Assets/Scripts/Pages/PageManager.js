function PageManager(tDependencies) {
    var vThis = this;
    vThis.PageElement = $(".Page");
    vThis.Page = ko.observable();
    vThis.ActivePage = ko.computed({
        read: function() {
            return vThis.Page();
        },
        write: function(tRoute) {
            var vRouteData = tRoute.split('/');
            if (vRouteData.length === 1 && vRouteData[0] === "") {
                vRouteData = ["Home", "Index"];
            }
            var vControllers = tDependencies.Controllers;
            var vController = vControllers[vRouteData[0]];
            var vArguments = vRouteData.slice(2);
            var vViewModel = vController[vRouteData[1]](vArguments);

            if (!vViewModel.IsVisible()) {
                vViewModel = vControllers.Home.Index();
                window.location.hash = "";
            }

            if (vThis.PageStillVisible) {
                vThis.PageStillVisible.dispose();
            }
            vThis.Page(vViewModel);
            vThis.PageStillVisible = vViewModel.IsVisible.subscribe(function(tData) {
                if (!tData) {
                    vThis.GoToPage();
                }
            });
            jQuery.validator.unobtrusive.parse(vThis.PageElement);
        },
        owner: vThis
    });    


    $(window).hashchange(function() {
        var vHash = window.location.hash.replace("#", "");
        vThis.ActivePage(vHash);
    });
}

PageManager.prototype = {
    Start: function() {
        var vThis = this;
        $(window).hashchange();
    },
    GoToPage: function() {
        var vHash = "";

        if (arguments.length > 0) {
            vHash = arguments[0];
        }

        for (var vIndex = 1; vIndex < arguments.length; vIndex++) {
            vHash += "/" + arguments[vIndex];
        }
        window.location.hash = vHash;
    }
};