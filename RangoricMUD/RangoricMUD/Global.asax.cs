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

using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Rangoric.Website.Accounts.Crews;
using Rangoric.Website.Bootstrappers;
using Rangoric.Website.Bootstrappers.Crews;
using Rangoric.Website.Web;
using RangoricMUD.App_Start;
using Raven.Client.Embedded;
using StackExchange.Profiling;

#endregion

namespace RangoricMUD
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Start();
        }
        public void Application_End()
        {
            End();
        }
        protected void Application_BeginRequest()
        {
            MiniProfiler.Start();
        }
        protected void Application_EndRequest()
        {
            MiniProfiler.Stop(true);
        }

        private IShip mShip;
        private void Start()
        {
            var mStore = new EmbeddableDocumentStore
            {
                ConnectionStringName = "RavenDB"
            };
            mStore.Initialize();

            MvcMiniProfiler.RavenDb.Profiler.AttachTo(mStore);
            mShip = new WindsorShip();
            mShip.Crew.Add(new DiceCrew());
            mShip.Crew.Add(new SecurityCrew());
            mShip.Crew.Add(new DatabaseCrew(mStore));
            mShip.Crew.Add(new AccountCrew());
            mShip.Crew.Add(new ControllerCrew());
            mShip.SetSail();
            ControllerBuilder.Current.SetControllerFactory(
                mShip.GetService(typeof(IControllerFactory)) as IControllerFactory);
            ModelBinders.Binders.DefaultBinder = new WindsorModelBinder(mShip);
        }
        private void End()
        {
            mShip.Dispose();
        }
    }
}