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

namespace RangoricMUD.Queries
{
    public abstract class BaseQuery<TType> : IQuery<TType>
    {
        #region IQuery<TType> Members

        public TType Result
        {
            get { return GetResult(); }
        }

        #endregion

        protected abstract TType GetResult();
    }
}