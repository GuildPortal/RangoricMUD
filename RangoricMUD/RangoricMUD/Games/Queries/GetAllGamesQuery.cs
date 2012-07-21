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
using System.Linq;
using RangoricMUD.Games.Data;
using RangoricMUD.Queries;
using Raven.Client;

#endregion

namespace RangoricMUD.Games.Queries
{
    public interface IGetAllGamesQuery : IQuery<List<Game>>
    {
    }

    public class GetAllGamesQuery : IGetAllGamesQuery
    {
        private readonly IDocumentStore mDocumentStore;

        public GetAllGamesQuery(IDocumentStore tDocumentStore)
        {
            mDocumentStore = tDocumentStore;
        }

        #region IGetAllGamesQuery Members

        public List<Game> Result
        {
            get
            {
                List<Game> vReturn;

                using(var vSession = mDocumentStore.OpenSession())
                {
                    vReturn = vSession.Query<Game>().ToList();
                }

                return vReturn;
            }
        }

        #endregion
    }
}