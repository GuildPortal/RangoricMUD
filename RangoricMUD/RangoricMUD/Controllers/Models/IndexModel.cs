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

using System.Web.Mvc;
using RangoricMUD.Accounts.Models;
using RangoricMUD.Games.Models;

#endregion

namespace RangoricMUD.Controllers.Models
{
    public class IndexModel
    {
        public IndexModel()
        {
            CreateAccount = new CreateAccount();
            LoginAccount = new LoginAccount();
            ConfirmAccountPageModel = new ConfirmAccountPageModel();
            GameListPageModel = new CreateGameModel();
        }

        public CreateAccount CreateAccount { get; private set; }
        public LoginAccount LoginAccount { get; private set; }

        public ConfirmAccountPageModel ConfirmAccountPageModel { get; private set; }

        public CreateGameModel GameListPageModel { get; set; }
    }
}