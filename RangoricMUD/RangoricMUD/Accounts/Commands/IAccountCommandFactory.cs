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

using RangoricMUD.Accounts.Models;
using RangoricMUD.Accounts.Queries;

#endregion

namespace RangoricMUD.Accounts.Commands
{
    public interface IAccountCommandFactory
    {
        INewAccountCommand CreateNewAccountCommand(CreateAccount tCreateAccount);
        ILoginAccountCommand CreateLoginAccountCommand(LoginAccount tLoginAccount);
        ICheckLoginQuery CreateCheckLoginQuery();
    }
}