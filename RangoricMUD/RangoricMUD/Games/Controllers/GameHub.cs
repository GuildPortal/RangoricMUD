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

using System.Collections.Generic;
using RangoricMUD.Games.Commands;
using RangoricMUD.Games.Data;
using RangoricMUD.Games.Models;
using RangoricMUD.Games.Queries;
using SignalR.Hubs;

#endregion

namespace RangoricMUD.Games.Controllers
{
    public class GameHub : Hub
    {
        private readonly IGameCommandFactory mCommandFactory;
        private readonly IGameQueryFactory mQueryFactory;

        public GameHub(IGameCommandFactory tCommandFactory, IGameQueryFactory tQueryFactory)
        {
            mCommandFactory = tCommandFactory;
            mQueryFactory = tQueryFactory;
        }

        public eGameCreationStatus CreateGame(CreateGameModel tCreateGameModel)
        {
            var vCommand = mCommandFactory.CreateCreateGameCommand(tCreateGameModel);

            return vCommand.Execute();
        }

        public List<Game> GetAll()
        {
            var vQuery = mQueryFactory.CreateGetAllGamesQuery();

            return vQuery.Result;
        }

        public Game GetGame(GetGameModel tGetGameModel)
        {
            var vQuery = mQueryFactory.CreateGetGameQuery(tGetGameModel);

            return vQuery.Result;
        }
    }
}