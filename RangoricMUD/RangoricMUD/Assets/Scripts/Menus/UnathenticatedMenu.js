var vServices = {
    Page: {
        Page: ko.observable('None'),
        PageData: ko.observable({ Name: 'None' })
    }
};

var PageChange = (function(tServices) {
    return function(tPage) {
        tServices.Page.Page(tPage.Name);
        tServices.Page.PageData(tPage);
    };
})(vServices);

function GenerateForm(tForm) {
    function GenerateFields(tFields) {
        var vResult = ko.observableArray();
        for (var vIndex = 0; vIndex < tFields.length; vIndex++) {
            var vItem = tFields[vIndex];

            var vField = {};
            vField.Name = vItem.Name;
            vField.Label = vItem.Label;
            vField.Type = "Input_" + vItem.Type;
            vField.Value = ko.observable(null);
            vField.IsVisible = ko.observable(true);
            if (vItem.MaxLength) {
                vField.Value.extend({
                    maxLength: {
                        params: vItem.MaxLength.Value,
                        message: vItem.MaxLength.Message
                    }
                });
            }

            if (vItem.Required) {
                vField.Value.extend({
                    required: {
                        params: vItem.Required.Value,
                        message: vItem.Required.Message
                    }
                });
            }

            if (vItem.MinLength) {
                vField.Value.extend({
                    minLength: {
                        params: vItem.MinLength.Value,
                        message: vItem.MinLength.Message
                    }
                });
            }

            vResult.push(ko.validatedObservable(vField));
        }

        return vResult;
    }
    function ConvertToForm(tObject) {
        var vObject = {};
        vObject.Legend = tObject.Legend;
        vObject.Fields = GenerateFields(tObject.Fields);
        vObject.IsValid = ko.computed(function () {
            var vGood = true;
            for (var vIndex = 0; vIndex < vObject.Fields().length; vIndex++) {
                var vField = vObject.Fields()[vIndex];
                vGood = vField.isValid() && vGood;
            }
            return vGood;
        });
        vObject.Submit = function () {
            if (vObject.IsValid()) {

            }
        };
        return vObject;
    }

    return ConvertToForm(tForm);
}

function MenuButtons() {
    var vMenu = {};
    vMenu.Buttons = ko.observableArray();

    vMenu.Buttons.push({
        Text: ko.observable("Login"),
        Click: function () {
            GetLoginFormCommand(function(tData) {
                var vPageData = {
                    Name: "LoginForm",
                    Form: GenerateForm(tData),
                };
                PageChange(vPageData);
            });
        },
        IsVisible: ko.observable(true)
    });

    return vMenu;
}

var MenuButtonFactory = (function() {
    return function() {
        return MenuButtons();
    };
})();

function UnAthenticatedMenu() {
    var vValidationOptions = { insertMessages: false };
    ko.validation.init(vValidationOptions);

    var vButtons = MenuButtonFactory();

    var vMenu = $("#Interface > div:first-child");
    var vInterface = $("#Interface > div:last-child");

    vMenu.attr("data-bind", "template:{ name:'Button', foreach:Buttons }");
    vInterface.attr("data-bind", "template:{name:Page(), data:PageData(), if:Page() === PageData().Name}");

    ko.applyBindings(vButtons, vMenu[0]);
    ko.applyBindings(vServices.Page, vInterface[0]);
    
}
