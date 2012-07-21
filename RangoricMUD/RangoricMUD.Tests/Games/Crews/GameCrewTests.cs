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

using System;
using NUnit.Framework;
using RangoricMUD.Bootstrappers;
using RangoricMUD.Bootstrappers.Crews;
using RangoricMUD.Games.Commands;
using RangoricMUD.Games.Controllers;
using RangoricMUD.Games.Models;
using RangoricMUD.Games.Queries;
using RangoricMUD.Tests.Utilities;
using Raven.Client;

#endregion

namespace RangoricMUD.Tests.Games.Crews
{
    [TestFixture]
    public class GameCrewTests : BaseTests
    {
        private WindsorShip mShip;
        private IDocumentStore mDocumentStore;

        [TestFixtureSetUp]
        public void Setup()
        {
            mShip = new WindsorShip();

            mDocumentStore = GetEmbeddedDatabase;
            mShip.Crew.Add(new DatabaseCrew(mDocumentStore));
            mShip.Crew.Add(new GameCrew());

            mShip.SetSail();
        }

        private void TestService(Type tType)
        {
            var vResult = mShip.GetService(tType);
            Assert.IsNotNull(vResult);
            mShip.ReleaseService(vResult);
        }

        [TestCase(typeof (IGameCommandFactory))]
        [TestCase(typeof (IGameQueryFactory))]
        [TestCase(typeof (IGetAllGamesQuery))]
        [TestCase(typeof (GameHub))]
        public void ServicesResolve(Type tType)
        {
            TestService(tType);
        }

        [Test]
        public void FactoryCanGenerateCreateGame()
        {
            var vFactory = (IGameCommandFactory) mShip.GetService(typeof (IGameCommandFactory));

            var vObject = vFactory.CreateCreateGameCommand(new CreateGameModel());

            Assert.IsNotNull(vObject);
        }

        [Test]
        public void FactoryCanGenerateGetGame()
        {
            var vFactory = (IGameQueryFactory) mShip.GetService(typeof (IGameQueryFactory));

            var vObject = vFactory.CreateGetGameQuery(new GetGameModel());

            Assert.IsNotNull(vObject);
        }
    }
}