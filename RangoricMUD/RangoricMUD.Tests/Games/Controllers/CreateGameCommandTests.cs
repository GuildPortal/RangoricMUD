using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RangoricMUD.Games.Commands;
using RangoricMUD.Games.Data;
using RangoricMUD.Games.Models;
using RangoricMUD.Tests.Utilities;
using Raven.Client.Embedded;

namespace RangoricMUD.Tests.Games.Controllers
{
    [TestFixture]
    public class CreateGameCommandTests
    {
        private EmbeddableDocumentStore mDocumentStore;

        [TestCase("ABC")]
        public void CreatesGameInRaven(string tName)
        {
            mDocumentStore = new EmbeddableDocumentStore { RunInMemory = true };
            mDocumentStore.Initialize();
            mDocumentStore.RegisterListener(new RavenDbNoStaleData());

            var vModel = new CreateGameModel() {Name = tName};

            var vCommand = new CreateGameCommand(vModel, mDocumentStore);
            vCommand.Execute();

            var vSession = mDocumentStore.OpenSession();
            var vObjects = vSession.Query<Game>().Where(t => t.Name == tName);

            Assert.AreEqual(1, vObjects.Count());
        }
    }
}
