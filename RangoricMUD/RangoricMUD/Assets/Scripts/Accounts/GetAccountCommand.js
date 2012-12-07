var GetAccountCommand = (function(tAjax) {
    return function(tCallback) {
        var vAjax = tAjax();
        vAjax
            .Url("/Accounts/Get")
            .IsJson()
            .IsPost()
            .Success(function(tData) {
                if (typeof tCallback !== 'undefined') {
                    tCallback(tData);
                }
            })
            .Start();

    };
})(Ajax);