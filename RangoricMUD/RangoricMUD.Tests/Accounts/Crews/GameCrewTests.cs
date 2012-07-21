using System;
using NUnit.Framework;
using RangoricMUD.Bootstrappers;
using RangoricMUD.Bootstrappers.Crews;
using RangoricMUD.Games.Commands;
using RangoricMUD.Games.Models;
using RangoricMUD.Games.Queries;
using RangoricMUD.Tests.Games.Crews;
using RangoricMUD.Tests.Utilities;
using Raven.Client;

namespace RangoricMUD.Tests.Accounts.Crews
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

        [TestCase(typeof(IGameCommandFactory))]
        [TestCase(typeof(IGameQueryFactory))]
        [TestCase(typeof(IGetAllGamesQuery))]
        public void ServicesResolve(Type tType)
        {
            TestService(tType);
        }
        [Test]
        public void FactoryCanGenerateCreateGame()
        {
            var vFactory = (IGameCommandFactory)mShip.GetService(typeof (IGameCommandFactory));

            var vObject = vFactory.CreateCreateGameCommand(new CreateGameModel());

            Assert.IsNotNull(vObject);
        }
        [Test]
        public void FactoryCanGenerateGetGame()
        {
            var vFactory = (IGameQueryFactory)mShip.GetService(typeof(IGameQueryFactory));

            var vObject = vFactory.CreateGetGameQuery(new GetGameModel());

            Assert.IsNotNull(vObject);
        }
    }
}