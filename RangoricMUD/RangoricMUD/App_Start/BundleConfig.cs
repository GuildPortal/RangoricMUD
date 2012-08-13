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

using System.Web.Optimization;

#endregion

namespace RangoricMUD.App_Start
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection tBundleCollection)
        {
            var vCssBundle =
                new Bundle("~/Assets/Styles", new LessMinify())
                    .Include("~/Assets/Styles/Reset.less",
                             "~/Assets/Styles/Theme.less");

            tBundleCollection.Add(vCssBundle);

            var vJavaScriptBundle =
                new ScriptBundle("~/Assets/Scripts")
                    .Include("~/Assets/Scripts/Libraries/jquery.js",
                             "~/Assets/Scripts/Libraries/jquery.validate.js",
                             "~/Assets/Scripts/Libraries/jquery.validate.unobtrusive.js",
                             "~/Assets/Scripts/Libraries/jquery.signalR.js",
                             "~/Assets/Scripts/Libraries/jquery.signalR.hubs.js",
                             "~/Assets/Scripts/Libraries/knockout.js")
                    .IncludeDirectory("~/Assets/Scripts/Components/", "*.js")
                    .IncludeDirectory("~/Assets/Scripts/Pages/", "*.js")
                    .IncludeDirectory("~/Assets/Scripts/Accounts/", "*.js")
                    .IncludeDirectory("~/Assets/Scripts/Administration/", "*.js")
                    .IncludeDirectory("~/Assets/Scripts/Games/", "*.js")
                    .Include("~/Assets/Scripts/Startup/Startup.js");

            tBundleCollection.Add(vJavaScriptBundle);
        }
    }
}