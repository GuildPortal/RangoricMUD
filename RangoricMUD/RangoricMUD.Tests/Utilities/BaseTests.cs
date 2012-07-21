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

using NUnit.Framework;
using RangoricMUD.Games.Commands;
using RangoricMUD.Games.Models;
using Raven.Client;
using Raven.Client.Embedded;

#endregion

namespace RangoricMUD.Tests.Utilities
{
    public class BaseTests
    {
        protected IDocumentStore GetEmbeddedDatabase
        {
            get
            {
                var vResult = new EmbeddableDocumentStore {RunInMemory = true};
                vResult.RegisterListener(new RavenDbNoStaleData());
                vResult.Initialize();
                return vResult;
            }
        }
        protected void AddGameToDatabase(string tGameName, IDocumentStore tDatabase)
        {
            var vModel = new CreateGameModel
                             {
                                 Name = tGameName
                             };
            var vCommand = new CreateGameCommand(tDatabase, vModel);
            Assert.AreEqual(eGameCreationStatus.Success, vCommand.Execute());
        }
    }
}