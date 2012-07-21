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

using Moq;
using NUnit.Framework;
using RangoricMUD.Games.Commands;
using RangoricMUD.Games.Controllers;
using RangoricMUD.Games.Models;
using RangoricMUD.Games.Queries;
using RangoricMUD.Tests.Utilities;

#endregion

namespace RangoricMUD.Tests.Games.Controllers
{
    [TestFixture]
    public class GameHubTests : BaseTests
    {
        [Test]
        public void CallsCommandExecuteToCreateGame()
        {
            var vModel = new CreateGameModel();

            var vFactory = new Mock<IGameCommandFactory>();
            var vCommand = new Mock<ICreateGameCommand>();
            vFactory.Setup(t => t.CreateCreateGameCommand(vModel)).Returns(vCommand.Object);

            var vGameHub = new GameHub(vFactory.Object, null);

            var vTask = vGameHub.CreateGame(vModel);

            vTask.Wait();

            vCommand.Verify(t => t.Execute());
        }

        [Test]
        public void CallsCommandFactoryToCreateGameCommand()
        {
            var vModel = new CreateGameModel();

            var vFactory = new Mock<IGameCommandFactory>();
            var vCommand = new Mock<ICreateGameCommand>();
            vFactory.Setup(t => t.CreateCreateGameCommand(vModel)).Returns(vCommand.Object);

            var vGameHub = new GameHub(vFactory.Object, null);

            var vTask = vGameHub.CreateGame(vModel);

            vTask.Wait();

            vFactory.VerifyAll();
        }

        [Test]
        public void CallsQueryFactoryToCreateGetAllQuery()
        {
            var vFactory = new Mock<IGameQueryFactory>();
            var vQuery = new Mock<IGetAllGamesQuery>();
            vFactory.Setup(t => t.CreateGetAllGamesQuery()).Returns(vQuery.Object);

            var vGameHub = new GameHub(null, vFactory.Object);
            var vTask = vGameHub.GetAll();

            vTask.Wait();

            vFactory.VerifyAll();
        }

        [Test]
        public void CallsQueryFactoryToCreateGetGameQuery()
        {
            var vModel = new GetGameModel();
            var vQuery = new Mock<IGetGameQuery>();
            var vFactory = new Mock<IGameQueryFactory>();
            vFactory.Setup(t => t.CreateGetGameQuery(vModel)).Returns(vQuery.Object);

            var vGameHub = new GameHub(null, vFactory.Object);
            var vTask = vGameHub.GetGame(vModel);

            vTask.Wait();

            vFactory.VerifyAll();
        }

        [Test]
        public void CallsQueryResultToGetAGame()
        {
            var vModel = new GetGameModel();
            var vQuery = new Mock<IGetGameQuery>();
            var vFactory = new Mock<IGameQueryFactory>();
            vFactory.Setup(t => t.CreateGetGameQuery(vModel)).Returns(vQuery.Object);

            var vGameHub = new GameHub(null, vFactory.Object);
            var vTask = vGameHub.GetGame(vModel);

            vTask.Wait();

            vQuery.VerifyGet(t => t.Result);
        }

        [Test]
        public void CallsQueryResultToGetGames()
        {
            var vFactory = new Mock<IGameQueryFactory>();
            var vQuery = new Mock<IGetAllGamesQuery>();
            vFactory.Setup(t => t.CreateGetAllGamesQuery()).Returns(vQuery.Object);

            var vGameHub = new GameHub(null, vFactory.Object);
            var vTask = vGameHub.GetAll();

            vTask.Wait();

            vQuery.VerifyGet(t => t.Result);
        }
    }
}