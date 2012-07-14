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
using System.Web.Mvc;

#endregion

namespace Rangoric.Website.Web
{
    public class WindsorModelBinder : DefaultModelBinder
    {
        private readonly IDependencyResolver mDependencyResolver;

        public WindsorModelBinder(IDependencyResolver tDependencyResolver)
        {
            mDependencyResolver = tDependencyResolver;
        }

        protected override object CreateModel(ControllerContext tControllerContext, ModelBindingContext tBindingContext,
                                              Type tModelType)
        {
            var vResult =
                tModelType.IsInterface
                    ? mDependencyResolver.GetService(tModelType)
                    : base.CreateModel(tControllerContext, tBindingContext, tModelType);
            return vResult;
        }
    }
}