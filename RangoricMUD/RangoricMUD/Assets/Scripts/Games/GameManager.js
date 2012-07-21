function GameManager(tSettings) {
    var vThis = this;
    vThis.Connection = tSettings.Connection;
}

GameManager.prototype = {
    Start: function () {
        var vThis = this;
        vThis.Hub = vThis.Connection.gameHub;
    }
}