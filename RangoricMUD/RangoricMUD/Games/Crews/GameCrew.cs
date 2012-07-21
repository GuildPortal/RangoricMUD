using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RangoricMUD.Bootstrappers.Crews;
using RangoricMUD.Games.Commands;
using RangoricMUD.Games.Queries;

namespace RangoricMUD.Tests.Games.Crews
{
    public class GameCrew : BaseCrew
    {
        protected override void StrapOn()
        {
            Add().OfInterface<IGetAllGamesQuery>().WithImplementation<GetAllGamesQuery>();
            Add().OfInterface<IGetGameQuery>().WithImplementation<GetGameQuery>();
            Add().OfInterface<IGameQueryFactory>().AsFactory();

            Add().OfInterface<ICreateGameCommand>().WithImplementation<CreateGameCommand>();
            Add().OfInterface<IGameCommandFactory>().AsFactory();
        }
    }
}
