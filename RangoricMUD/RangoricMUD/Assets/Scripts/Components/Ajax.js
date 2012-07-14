function Ajax() {
    var vThis = this;
    vThis.Settings = new AjaxSettings();
}

Ajax.prototype = {
    Start: function() {
        var vThis = this;
        $.ajax(vThis.Settings);
    }
};
function AjaxSettings() {
    var vThis = this;
}

AjaxSettings.prototype = {
    Url: function(tUrl) {
        var vThis = this;
        vThis.url = tUrl;
        return vThis;
    },
    Type: function(tType) {
        var vThis = this;
        vThis.type = tType;
        return vThis;
    },
    Success: function(tSuccess) {
        var vThis = this;
        vThis.success = tSuccess;
        return vThis;
    },
    IsPost:function() {
        var vThis = this;
        vThis.Type("POST");
        return vThis;
    },
    IsGet: function () {
        var vThis = this;
        vThis.Type("GET");
        return vThis;
    },
    IsJson:function () {
        var vThis = this;
        vThis.datatype = 'json';
        return vThis;
    },
    UsingData:function (tData) {
        var vThis = this;
        vThis.data = tData;
        return vThis;
    }
};