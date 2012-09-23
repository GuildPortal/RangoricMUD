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
    vThis.LoadCharacters = function(tGameName) {
        vHub.loadAll(tGameName);
    };

    vHub.LoadCharacters = function(tData) {
        for(var vIndex = 0;vIndex < tData.length;vIndex++) {
            var vRaw = tData[vIndex];
            var vCharacter = {
                ListName: ko.observable(vRaw.ListName),
                Name: ko.observable(vRaw.Name)
            };
            vThis.Characters.push(vCharacter);
        }
    };
    
    return vThis;
}