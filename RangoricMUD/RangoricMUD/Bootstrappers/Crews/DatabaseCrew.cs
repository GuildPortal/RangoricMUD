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

using Raven.Client;

#endregion

namespace Rangoric.Website.Bootstrappers.Crews
{
    public class DatabaseCrew : BaseCrew
    {
        private readonly IDocumentStore mDocumentStore;

        public DatabaseCrew(IDocumentStore tDocumentStore)
        {
            mDocumentStore = tDocumentStore;
        }

        protected override void StrapOn()
        {
            Add().OfInterface<IDocumentStore>().UsingInstance(mDocumentStore).WithLifeStyle(eLifeStyle.Singleton);
        }
    }
}