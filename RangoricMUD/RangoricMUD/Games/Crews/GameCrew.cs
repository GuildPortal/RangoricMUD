#region License

// RangoricMUD is licensed under the Open Game License.
// The original code and assets provided in this repository are Open Game Content,
// The name RangoricMUD is product identity, and can only be used as a part of the code,
//   or in reference to this project.
// 
// More details and the full text of the license are available at:
//   https://github.com/Rangoric/RangoricMUD/wiki/Open-Game-License
// 
// RangoricMUD's home is at: https://github.com/Rangoric/RangoricMUD

#endregion

#region References

using RangoricMUD.Bootstrappers.Crews;
using RangoricMUD.Games.Commands;
using RangoricMUD.Games.Controllers;
using RangoricMUD.Games.Queries;

#endregion

namespace RangoricMUD.Tests.Games.Crews
{
    public class GameCrew : BaseCrew
    {
        protected override void StrapOn()
        {
            Add().OfInterface<GameHub>().WithImplementation<GameHub>();

            Add().OfInterface<IGetAllGamesQuery>().WithImplementation<GetAllGamesQuery>();
            Add().OfInterface<IGetGameQuery>().WithImplementation<GetGameQuery>();
            Add().OfInterface<IGameQueryFactory>().AsFactory();

            Add().OfInterface<ICreateGameCommand>().WithImplementation<CreateGameCommand>();
            Add().OfInterface<IGameCommandFactory>().AsFactory();
        }
    }
}