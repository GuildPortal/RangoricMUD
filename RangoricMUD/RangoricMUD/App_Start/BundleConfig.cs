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
                             "~/Assets/Scripts/Libraries/knockout.js",
                             "~/Assets/Scripts/Settings/Urls.js",
                             "~/Assets/Scripts/Components/Button.js",
                             "~/Assets/Scripts/Components/Ajax.js",
                             "~/Assets/Scripts/Components/PageManager.js",
                             "~/Assets/Scripts/Accounts/AccountManager.js",
                             "~/Assets/Scripts/Accounts/LoginPage.js",
                             "~/Assets/Scripts/Accounts/LoginButton.js",
                             "~/Assets/Scripts/Accounts/CreateAccountPage.js",
                             "~/Assets/Scripts/Accounts/CreateAccountButton.js",
                             "~/Assets/Scripts/Accounts/ConfirmAccountPage.js",
                             "~/Assets/Scripts/Accounts/ConfirmAccountButton.js",
                             "~/Assets/Scripts/Administration/AdminManager.js",
                             "~/Assets/Scripts/Administration/AdminButton.js",
                             "~/Assets/Scripts/Games/GameManager.js",
                             "~/Assets/Scripts/Games/GameListPage.js",
                             "~/Assets/Scripts/Games/GameListButton.js",
                             "~/Assets/Scripts/Games/GameEditPage.js",
                             "~/Assets/Scripts/Startup.js");

            tBundleCollection.Add(vJavaScriptBundle);
        }
    }
}