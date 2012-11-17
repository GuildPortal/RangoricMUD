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
                .IncludeDirectory("~/Assets/Scripts/", "*.js", true);

            tBundleCollection.Add(vLibraryScriptBundle);
            tBundleCollection.Add(vOtherScripts);
        }
    }
}