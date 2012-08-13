// ePages
//  Used to easily change the main page being used.
var ePages = {
    HomePage: { },
    ConfirmAccountPage: { },
    CreateAccount: { },
    LoginPage: {},
    GameListPage: {},
    GameEditPage:{},
    Setup: function(tDependencies) {
        ePages.HomePage.ViewModel = new HomePage(tDependencies);
        ePages.ConfirmAccountPage.ViewModel = new ConfirmAccountPage(tDependencies);
        ePages.CreateAccount.ViewModel = new CreateAccountPage(tDependencies);
        ePages.LoginPage.ViewModel = new LoginPage(tDependencies);
        ePages.GameListPage.ViewModel = new GameListPage(tDependencies);
        ePages.GameEditPage.ViewModel = new GameEditPage(tDependencies);
    }
};