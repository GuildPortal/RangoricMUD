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

using RangoricMUD.Commands;
using RangoricMUD.Games.Data;
using RangoricMUD.Games.Models;
using Raven.Abstractions.Exceptions;
using Raven.Client;

#endregion

namespace RangoricMUD.Games.Commands
{
    public class CreateGameCommand : BaseCommand<eGameCreationStatus>, ICreateGameCommand
    {
        private readonly CreateGameModel mCreateGameModel;
        private readonly IDocumentStore mDocumentStore;

        public CreateGameCommand(IDocumentStore tDocumentStore, CreateGameModel tCreateGameModel)
        {
            mCreateGameModel = tCreateGameModel;
            mDocumentStore = tDocumentStore;
        }

        #region ICreateGameCommand Members

        public override eGameCreationStatus Execute()
        {
            using (var vSession = mDocumentStore.OpenSession())
            {
                vSession.Advanced.UseOptimisticConcurrency = true;

                var vGame = new Game {Name = mCreateGameModel.Name};

                vSession.Store(vGame, "Games/" + vGame.Name);

                try
                {
                    vSession.SaveChanges();
                }
                catch (ConcurrencyException)
                {
                    return eGameCreationStatus.DuplicateName;
                }
            }

            return eGameCreationStatus.Success;
        }

        #endregion
    }
}