var GetLoginFormCommand = (function (tAjax) {
    return function (tSuccess, tError) {
        var vAjax = tAjax();
        vAjax
            .Url("/Forms/Login")
            .IsGet()
            .IsJson()
            .Success(tSuccess)
            .Error(tError)
            .Start();
        return vAjax;
    };
})(Ajax);