using System.Web.Optimization;
using dotless.Core;

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
}