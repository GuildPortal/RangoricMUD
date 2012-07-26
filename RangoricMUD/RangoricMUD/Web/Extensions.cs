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
            var vPage = new ViewPage<TType>
                            {
                                ViewContext = new ViewContext(),
                                Url = GetUrlHelper(),
                                ViewData = new ViewDataDictionary<TType>(tModel)
                            };

            vPage.Controls.Add(vPage.LoadControl(tControlName));

            var vBuilder = new StringBuilder();
            using(var vWriter = new StringWriter(vBuilder))
            {
                using(var vHtmlWriter = new HtmlTextWriter(vWriter))
                {
                    vPage.RenderControl(vHtmlWriter);
                }
            }
            return vBuilder.ToString();
        }

        private static UrlHelper GetUrlHelper()
        {
            var vContext = HttpContext.Current;

            if(vContext == null)
            {
                var vRequest = new HttpRequest("/", "http://www.test.com", "");
                var vResponse = new HttpResponse(new StringWriter());
                vContext = new HttpContext(vRequest, vResponse);
            }

            var vContextBase = new HttpContextWrapper(vContext);
            var vData = new RouteData();
            var vRequestContext = new RequestContext(vContextBase, vData);
            return new UrlHelper(vRequestContext);
        }
    }
}