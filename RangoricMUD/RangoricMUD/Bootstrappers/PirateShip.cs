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
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using System.Web.Routing;

#endregion

namespace Rangoric.Website.Bootstrappers
{
    public class PirateShip : IShip
    {
        protected readonly ICaptain mCaptain;

        public PirateShip(ICaptain tCaptain, IEnumerable<ICrew> tCrew)
        {
            mCaptain = tCaptain;
            Crew = tCrew.ToList();
        }

        #region IShip Members

        public List<ICrew> Crew { get; private set; }

        public object GetService(Type tServiceType)
        {
            return mCaptain.Resolve(tServiceType);
        }

        public IEnumerable<object> GetServices(Type tServiceType)
        {
            return mCaptain.ResolveAll(tServiceType);
        }

        public void ReleaseService<TType>(TType tObject)
        {
            mCaptain.Release(tObject);
        }

        public IController Create(RequestContext tRequestContext, Type tControllerType)
        {
            IController vController = null;
            if (tControllerType != null)
            {
                vController = GetService(tControllerType) as IController;
            }
            return vController;
        }

        public void SetSail()
        {
            mCaptain.InspectCrew(Crew);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        public void Register(Type tServiceType, Func<object> tActivator)
        {
            throw new NotImplementedException();
        }

        public void Register(Type tServiceType, IEnumerable<Func<object>> tActivators)
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool tAll)
        {
            mCaptain.Dispose();
        }
    }
}