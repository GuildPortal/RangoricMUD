/* Game Class */

function Game(tGame) {
    var vGame = {
        Name: ko.computed({
            read: function () {
                
            },
            write: function (tName) {
                
            }
        })
    };
    return vGame;
}
function Game(tGame) {
    var vGame = {
        Name: ko.observable(tGame.Name),
        Areas: ko.observableArray()
    };

    return vGame;
}
function Area() {
    var vArea = {
        
    };

    return vArea;
}