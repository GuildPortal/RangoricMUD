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

#endregion

namespace RangoricMUD.Bootstrappers
{
    public class Boot : IBoot
    {
        #region IBoot Members

        public eBootType BootType { get; private set; }

        public Type Interface { get; private set; }
        public Type Implementation { get; private set; }
        public eLifeStyle LifeStyle { get; private set; }
        public object Instance { get; private set; }

        public IBoot OfInterface<TType>()
        {
            Interface = typeof (TType);
            return this;
        }

        public IBoot WithImplementation<TType>()
        {
            Implementation = typeof (TType);
            return this;
        }

        public IBoot WithLifeStyle(eLifeStyle tLifeStyle)
        {
            LifeStyle = tLifeStyle;
            return this;
        }

        public IBoot AsFactory()
        {
            BootType = eBootType.Factory;
            return this;
        }

        public IBoot UsingInstance(object tObject)
        {
            Instance = tObject;
            BootType = eBootType.Instance;
            return this;
        }

        #endregion
    }
}