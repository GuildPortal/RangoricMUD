$(function () {
    GetAccountCommand(function(tData) {
        if (tData !== "") {

        } else {
            UnAthenticatedMenu();
        }
    });
});