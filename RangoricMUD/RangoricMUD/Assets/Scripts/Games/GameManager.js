﻿function GameManager(tSettings) {
    var vThis = this;

    tSettings.Connection.hub.stateChanged(function (tChange) {
        if (tChange.newState === $.signalR.connectionState.connected) {
            vThis.Hub = tSettings.Connection.gameHub;
        }
    });
}

GameManager.prototype = {
    
}