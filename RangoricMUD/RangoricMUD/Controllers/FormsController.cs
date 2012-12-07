using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RangoricMUD.Controllers
{
    public class FormsController : BaseController
    {
        [HttpGet]
        public async Task<JsonResult> Login()
        {
            var vForm = new
                            {
                                Legend = "Login to your Account",
                                Fields = new object[2]
                            };
            vForm.Fields[0] = new
                                  {
                                      Name = "Name",
                                      Label = "Name",
                                      Type = "ShortString",
                                      MinLength = new
                                      {
                                          Value = 3,
                                          Message = "A"
                                      },
                                      MaxLength = new
                                      {
                                          Value = 100,
                                          Message = "B"
                                      },
                                      Required = new
                                      {
                                          Value = true,
                                          Message = "C"
                                      }
                                  };
            vForm.Fields[1] = new
                                  {
                                      Name = "Password",
                                      Label = "Password",
                                      Type = "Password",
                                      MinLength = new
                                      {
                                          Value = 10,
                                          Message = "A"
                                      },
                                      MaxLength = new
                                      {
                                          Value = 100,
                                          Message = "B"
                                      },
                                      Required = new
                                      {
                                          Value = true,
                                          Message = "C"
                                      }
                                  };

            return Json(vForm, JsonRequestBehavior.AllowGet);
        }
    }
}
