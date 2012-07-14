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
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

#endregion

namespace Rangoric.Website.Bootstrappers
{
    public class ControllerFactoryShip : DefaultControllerFactory
    {
        private readonly IShip mShip;

        public ControllerFactoryShip(IShip tShip)
        {
            mShip = tShip;
        }

        protected override IController GetControllerInstance(RequestContext tRequestContext, Type tControllerType)
        {
            if (tControllerType == null)
            {
                throw new HttpException(404, "That path doesn't mean anything to me. I'm sorry.");
            }
            return mShip.GetService(tControllerType) as IController;
        }

        public override void ReleaseController(IController tController)
        {
            mShip.ReleaseService(tController);
        }
    }
}