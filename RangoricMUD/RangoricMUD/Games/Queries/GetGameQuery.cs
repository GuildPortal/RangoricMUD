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

using RangoricMUD.Games.Data;
using RangoricMUD.Games.Models;
using RangoricMUD.Queries;
using Raven.Client;

#endregion

namespace RangoricMUD.Games.Queries
{
    public interface IGetGameQuery : IQuery<Game>
    {
    }

    public class GetGameQuery : IGetGameQuery
    {
        private readonly IDocumentStore mDocumentStore;
        private readonly GetGameModel mGetGameModel;

        public GetGameQuery(IDocumentStore tDocumentStore, GetGameModel tGetGameModel)
        {
            mDocumentStore = tDocumentStore;
            mGetGameModel = tGetGameModel;
        }

        #region IGetGameQuery Members

        public Game Result
        {
            get
            {
                Game vResult;

                using (var vSession = mDocumentStore.OpenSession())
                {
                    vResult = vSession.Load<Game>(mGetGameModel.Name);
                }

                return vResult;
            }
        }

        #endregion
    }
}