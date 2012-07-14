function CreateAccountPage() {
    var vThis = this;
    vThis.Name = ko.observable("Create Account");
}

CreateAccountPage.prototype = {
    Submit: function (tForm) {
        
    }
};

ePages.CreateAccount = {
    ViewModel: new CreateAccountPage()
};