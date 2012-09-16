function CharactersManager(tDependencies) {
    var vConnection = tDependencies.Connection;
    var vHub = vConnection.charactersHub;
    var vThis = {
        Characters: ko.observableArray(),
        Character: ko.observable()
    };

    vThis.Create = function(tObject) {
        vHub.create(tObject)
            .done(function (tData) {
                if(tData) {
                    vThis.Characters.push(tData);
                    vThis.Character(tData);
                }
            });
    };
    
    return vThis;
}