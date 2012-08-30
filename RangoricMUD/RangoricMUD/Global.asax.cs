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
using RangoricMUD.Accounts.Crews;
using RangoricMUD.App_Start;
using RangoricMUD.Bootstrappers;
using RangoricMUD.Bootstrappers.Crews;
using RangoricMUD.Tests.Games.Crews;
using RangoricMUD.Web;
using RangoricMUD.Web.Crews;
using Raven.Client.Embedded;
using SignalR;
using SignalR.Hubs;

#endregion

namespace RangoricMUD
{
    public class MvcApplication : HttpApplication
    {
        private IShip mShip;

        protected void Application_Start()
        {
            Start();
            GlobalHost.DependencyResolver.Register(typeof (IHubActivator), () => mShip);

            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_End()
        {
            End();
        }

        protected void Application_BeginRequest()
        {

        }

        protected void Application_EndRequest()
        {
        }

        private void Start()
        {
            var vStore = new EmbeddableDocumentStore
                             {
                                 ConnectionStringName = "RAVENHQ_CONNECTION_STRING"
                             };
            vStore.Initialize();

            mShip = new WindsorShip();
            mShip.Crew.Add(new DiceCrew());
            mShip.Crew.Add(new SecurityCrew());
            mShip.Crew.Add(new DatabaseCrew(vStore));
            mShip.Crew.Add(new AccountCrew());
            mShip.Crew.Add(new ControllerCrew());
            mShip.Crew.Add(new GameCrew());
            mShip.Crew.Add(new WebCrews());
            mShip.SetSail();

            ControllerBuilder.Current.SetControllerFactory(
                mShip.GetService(typeof (IControllerFactory)) as IControllerFactory);

            ModelBinders.Binders.DefaultBinder = new WindsorModelBinder(mShip);
        }

        private void End()
        {
            mShip.Dispose();
        }
    }
}