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

using System.Threading.Tasks;
using System.Web.Mvc;

#endregion

namespace RangoricMUD.Controllers
{
    public class HomeController : BaseController
    {
        //[OutputCache(Duration = 3600)]
        public async Task<ViewResult> Index()
        {
            return View("Index");
        }

        public async Task<ViewResult> Playground()
        {
            return View("Playground");
        }
    }
}