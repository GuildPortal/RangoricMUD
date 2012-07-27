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

using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;

namespace RangoricMUD.Web
{
    public static class Extensions
    {
        public static string RenderPartialToString<TType>(string tControlName, TType tModel)
        {
            var vEngine = new RazorViewEngine();
            var vTemp = new RazorEngine<RazorTemplateBase>();
            vEngine.C;
            var vView = new RazorView(new ControllerContext(), tControlName, null, false, null);

            var vBuilder = new StringBuilder();
            using(var vWriter = new StringWriter(vBuilder))
            {
                vView.Render(new ViewContext() { ViewData = new ViewDataDictionary(tModel) }, vWriter);
            }
            return vBuilder.ToString();
        }
    }
}