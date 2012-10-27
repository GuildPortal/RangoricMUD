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
            var vLibraryScriptBundle =
                new ScriptBundle("~/Assets/Scripts/LibraryScripts")
                    .IncludeDirectory("~/Scripts/", "*.js");

            var vOtherScripts = new ScriptBundle("~/Assets/Scripts/OtherScripts")
                .IncludeDirectory("~/Assets/Scripts/Components/", "*.js")
                .IncludeDirectory("~/Assets/Scripts/Pages/", "*.js")
                .IncludeDirectory("~/Assets/Scripts/Accounts/", "*.js")
                .IncludeDirectory("~/Assets/Scripts/Administration/", "*.js")
                .IncludeDirectory("~/Assets/Scripts/Games/", "*.js")
                .IncludeDirectory("~/Assets/Scripts/Characters/", "*.js")
                .IncludeDirectory("~/Assets/Scripts/Graphics/", "*.js")
                .Include("~/Assets/Scripts/Startup/Startup.js");

            tBundleCollection.Add(vLibraryScriptBundle);
            tBundleCollection.Add(vOtherScripts);
        }
    }
}