#region LIcense

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
using System.Threading.Tasks;
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

        public Task CreateGame(CreateGameModel tCreateGameModel)
        {
            return
                Task.Factory.StartNew(
                    () =>
                        {
                            var
                                vCommand = mCommandFactory.CreateCreateGameCommand(tCreateGameModel);

                            vCommand.Execute();
                        });
        }

        public Task GetAll()
        {
            return
                Task.Factory.StartNew(
                    () =>
                        {
                            var vQuery = mQueryFactory.CreateGetAllGamesQuery();

                            Caller.AddGames(vQuery.Result);
                        });
        }

        public Task GetGame(GetGameModel tGetGameModel)
        {
            return
                Task.Factory.StartNew(
                    () =>
                        {
                            var vQuery = mQueryFactory.CreateGetGameQuery(tGetGameModel);

                            Caller.AddGame(vQuery.Result);
                        });
        }
    }
}