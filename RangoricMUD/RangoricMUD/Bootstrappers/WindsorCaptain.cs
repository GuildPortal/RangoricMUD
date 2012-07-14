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
using Castle.Core;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;

#endregion

namespace Rangoric.Website.Bootstrappers
{
    public class WindsorCaptain : PirateCaptain, ICaptain
    {
        private readonly IWindsorContainer mContainer;

        public WindsorCaptain(IWindsorContainer tContainer = null)
        {
            mContainer = tContainer ?? new WindsorContainer();
            mContainer.AddFacility<TypedFactoryFacility>();
            mContainer.Kernel.Resolver.AddSubResolver(new ArrayResolver(mContainer.Kernel));
        }

        #region ICaptain Members

        public object Resolve(Type tServiceType)
        {
            object vResult = null;
            try
            {
                vResult = mContainer.Resolve(tServiceType);
            }
            catch (Exception)
            {
                //We should return null when there is an error.
            }
            return vResult;
        }

        public IEnumerable<object> ResolveAll(Type tServiceType)
        {
            return mContainer.ResolveAll(tServiceType).Cast<object>();
        }

        public void Release(object tObject)
        {
            mContainer.Release(tObject);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        ///   Registers an object of TType as a singleton
        /// </summary>
        /// <typeparam name="TType"> </typeparam>
        /// <param name="tObject"> </param>
        public void Register<TType>(TType tObject) where TType : class
        {
            mContainer.Register(Component.For<TType>().Instance(tObject).LifestyleSingleton());
        }

        private LifestyleType FromLifestyleToLifestyleType(eLifeStyle tLifeStyle)
        {
            var vReturn = LifestyleType.Transient;
            switch (tLifeStyle)
            {
                case eLifeStyle.Singleton:
                    vReturn = LifestyleType.Singleton;
                    break;
            }
            return vReturn;
        }

        protected override void InspectBoots(List<IBoot> tBoots)
        {
            InspectBasics(tBoots);
            InspectInstances(tBoots);
            InspectFactories(tBoots);
        }

        private void InspectBasics(List<IBoot> tBoots)
        {
            foreach (var vBoot in tBoots.Where(t => t.BootType == eBootType.Basic))
            {
                var vLifestyle = FromLifestyleToLifestyleType(vBoot.LifeStyle);
                mContainer.Register(
                    Component
                        .For(vBoot.Interface)
                        .ImplementedBy(vBoot.Implementation)
                        .LifeStyle.Is(vLifestyle));
            }
        }

        private void InspectInstances(IEnumerable<IBoot> tBoots)
        {
            foreach (var vBoot in tBoots.Where(t => t.BootType == eBootType.Instance))
            {
                var vLifeStyle = FromLifestyleToLifestyleType(vBoot.LifeStyle);
                mContainer.Register(Component.For(vBoot.Interface).Instance(vBoot.Instance).LifeStyle.Is(vLifeStyle));
            }
        }

        private void InspectFactories(IEnumerable<IBoot> tBoots)
        {
            foreach (var vBoot in tBoots.Where(t => t.BootType == eBootType.Factory))
            {
                var vLifeStyle = FromLifestyleToLifestyleType(vBoot.LifeStyle);
                mContainer.Register(
                    Component.For(vBoot.Interface)
                        .AsFactory()
                        .LifeStyle.Is(vLifeStyle));
            }
        }

        protected override void InspectBootCollections(List<IBoot> tBoots)
        {
            foreach (var vBoot in tBoots)
            {
                var vLifestyle = FromLifestyleToLifestyleType(vBoot.LifeStyle);
                mContainer.Register(
                    AllTypes.FromAssemblyContaining(vBoot.Implementation)
                        .BasedOn(vBoot.Interface)
                        .WithService.FromInterface(vBoot.Interface)
                        .Configure(t => t.LifeStyle.Is(vLifestyle)));
            }
        }

        protected virtual void Dispose(bool tAll)
        {
            mContainer.Dispose();
        }
    }
}