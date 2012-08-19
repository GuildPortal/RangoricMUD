function Controllers(tDependencies) {
    var vThis = this;
    return {
        Home: HomeController(tDependencies),
        Accounts: AccountsController(tDependencies),
        Games: GamesController(tDependencies)
    };
}