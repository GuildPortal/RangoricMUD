function Controllers(tDependencies) {
    return {
        Home: HomeController(tDependencies),
        Accounts: AccountsController(tDependencies),
        Games: GamesController(tDependencies),
        Play: PlayController(tDependencies),
        Characters: CharactersController(tDependencies),
        Areas: AreasController(tDependencies)
    };
}