function Ajax() {
    var vSettings = {};
    var vAjax = null;

    var vThis = {
    };
    vThis.Start = function() {
        vAjax = $.ajax(vSettings);
        return vThis;
    };
    vThis.Stop = function() {
        if (vAjax !== null && vAjax.abort) {
            vAjax.abort();
        }
        return vThis;
    };
    vThis.Url = function(tUrl) {
        vSettings.url = tUrl;
        return vThis;
    };
    vThis.Type = function(tType) {
        vSettings.type = tType;
        return vThis;
    };
    vThis.Success = function(tCallback) {
        vSettings.success = tCallback;
        return vThis;
    };
    vThis.Error = function(tCallback) {
        vSettings.error = tCallback;
        return vThis;
    };
    vThis.IsPost = function() {
        vThis.Type("POST");
        return vThis;
    };
    vThis.IsGet = function() {
        vThis.Type("GET");
        return vThis;
    };
    vThis.IsJson = function() {
        vSettings.datatype = "json";
        return vThis;
    };
    vThis.UsingData = function(tData) {
        vSettings.data = tData;
        return vThis;
    };
    return vThis;
}