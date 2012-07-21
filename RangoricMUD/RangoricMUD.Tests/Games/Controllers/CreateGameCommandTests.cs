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
        

        [TestCase("ABC")]
        public void CreatesGameInRaven(string tName)
        {
            var vDocumentStore = new EmbeddableDocumentStore { RunInMemory = true };
            vDocumentStore.Initialize();
            vDocumentStore.RegisterListener(new RavenDbNoStaleData());

            var vModel = new CreateGameModel() {Name = tName};

            var vCommand = new CreateGameCommand(vModel, vDocumentStore);
            vCommand.Execute();

            var vSession = vDocumentStore.OpenSession();
            var vObjects = vSession.Query<Game>().Where(t => t.Name == tName);

            Assert.AreEqual(1, vObjects.Count());
        }
    }
}
