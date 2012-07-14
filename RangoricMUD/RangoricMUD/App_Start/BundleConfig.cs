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
using dotless.Core;

#endregion

namespace RangoricMUD.App_Start
{
    internal class LessMinify : CssMinify
    {
        public override void Process(BundleContext context, BundleResponse vResponse)
        {
            vResponse.Content = Less.Parse(vResponse.Content);
            base.Process(context, vResponse);
        }
    }

    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection tBundleCollection)
        {
            var vCssBundle = new Bundle("~/Assets/Stylesheets", new LessMinify());
            vCssBundle.IncludeDirectory("~/Assets/Stylesheets", "Reset.less");
            vCssBundle.IncludeDirectory("~/Assets/Stylesheets", "Theme.less");
            tBundleCollection.Add(vCssBundle);

            var vJavaScriptBundle = new Bundle("~/Assets/JavaScript", new JsMinify());

            vJavaScriptBundle.IncludeDirectory("~/Assets/JavaScript/Libraries", "jquery.js");
            vJavaScriptBundle.IncludeDirectory("~/Assets/JavaScript/Libraries", "jquery.validate.js");
            vJavaScriptBundle.IncludeDirectory("~/Assets/JavaScript/Libraries", "jquery.validate.unobtrusive.js");
            vJavaScriptBundle.IncludeDirectory("~/Assets/JavaScript/Libraries", "knockout.js");

            vJavaScriptBundle.IncludeDirectory("~/Assets/JavaScript/Settings", "Urls.js");

            vJavaScriptBundle.IncludeDirectory("~/Assets/JavaScript/Components", "Button.js");
            vJavaScriptBundle.IncludeDirectory("~/Assets/JavaScript/Components", "Ajax.js");
            vJavaScriptBundle.IncludeDirectory("~/Assets/JavaScript/Components", "PageManager.js");

            vJavaScriptBundle.IncludeDirectory("~/Assets/JavaScript/Accounts", "AccountManager.js");
            vJavaScriptBundle.IncludeDirectory("~/Assets/JavaScript/Accounts", "LoginPage.js");
            vJavaScriptBundle.IncludeDirectory("~/Assets/JavaScript/Accounts", "LoginButton.js");
            vJavaScriptBundle.IncludeDirectory("~/Assets/JavaScript/Accounts", "CreateAccountPage.js");
            vJavaScriptBundle.IncludeDirectory("~/Assets/JavaScript/Accounts", "CreateAccountButton.js");

            vJavaScriptBundle.IncludeDirectory("~/Assets/JavaScript/Administration", "AdminButton.js");

            vJavaScriptBundle.IncludeDirectory("~/Assets/JavaScript", "Startup.js");

            tBundleCollection.Add(vJavaScriptBundle);
        }
    }
}