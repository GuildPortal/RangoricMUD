var vServices = {
    Page: {
        Page: ko.observable('None'),
        PageData: ko.observable({ Name: 'None' })
    }
};

function ConvertToForm(tObject) {
    var vObject = {};
    vObject.Submit = function() {
    };
    vObject.Legend = "Hello";
    vObject.Fields = ko.observableArray();
    for (var vIndex = 0; vIndex < tObject.Fields.length; vIndex++) {
        var vItem = tObject.Fields[vIndex];

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

        vObject.Fields.push(vField);
    }
    return vObject;
}

var GetLoginFormCommand = (function(tAjax) {
    return function(tSuccess, tError) {
        var vAjax = tAjax();
        vAjax
            .Url("/Forms/Login")
            .IsGet()
            .IsJson()
            .Success(tSuccess)
            .Error(tError)
            .Start();
        return vAjax;
    };
})(Ajax);

function MenuButtons(tServices) {
    var vMenu = {};
    vMenu.Buttons = ko.observableArray();

    vMenu.Buttons.push({
        Text: ko.observable("Login"),
        Click: function () {
            GetLoginFormCommand(function(tData) {
                var vPageName = "LoginForm";

                var vPageData = {
                    Name: "LoginForm",
                    Form: ConvertToForm(tData),
                };

                tServices.Page.Page(vPageName);
                tServices.Page.PageData(vPageData);
            });
        },
        IsVisible: ko.observable(true)
    });

    return vMenu;
}

var MenuButtonFactory = (function(tServices) {
    return function() {
        return MenuButtons(tServices);
    };
})(vServices);

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
