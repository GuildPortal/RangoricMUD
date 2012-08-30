function ServerStatusManager(tDependencies) {
    var vThis = {
        IsRavenConnected: ko.observable(false),
        CheckStatus: function () {
            tDependencies.Connection.serverStatus.startStatusCheck();
        }
    };
    tDependencies.Connection.serverStatus.UpdateRavenStatus = function(tData) {
        vThis.IsRavenConnected(tData);
    };
    return vThis;
}