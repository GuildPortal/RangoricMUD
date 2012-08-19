function HomeController() {
    var vThisController = this;
    return {
        Index: function() {
            return {
                Page: ko.observable("Home/Index"),
                IsVisible: ko.observable(true)
            };
        }
    };
}