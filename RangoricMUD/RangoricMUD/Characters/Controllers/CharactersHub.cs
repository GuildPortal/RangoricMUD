#region LIcense

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using RangoricMUD.Characters.Commands;
using RangoricMUD.Characters.Models;
using RangoricMUD.Security;
using SignalR.Hubs;

#endregion

namespace RangoricMUD.Characters.Controllers
{
    public class CharactersHub : Hub
    {
        private readonly ICharactersCommandFactory mCharacterCommandFactory;
        private readonly ISignInPersistance mSignInPersistance;

        public CharactersHub(ICharactersCommandFactory tCharacterCommandFactory, ISignInPersistance tSignInPersistance)
        {
            mCharacterCommandFactory = tCharacterCommandFactory;
            mSignInPersistance = tSignInPersistance;
        }

        public Task<bool> Create(CreateCharacterModel tModel)
        {
            return Task.Factory.StartNew(
                () =>
                    {
                        var vAccountName = mSignInPersistance.AccountName(Context.ConnectionId);
                        var vCommand =
                            mCharacterCommandFactory.CreateCreateCharacterCommand(tModel,
                                                                                  vAccountName);
                        return vCommand.Execute() == eCharacterCreationStatus.Success;
                    });
        }

        public Task LoadAll(string tGameName)
        {
            return Task.Factory.StartNew(
                () =>
                    {
                        var vAccountName = mSignInPersistance.AccountName(Context.ConnectionId);
                        var vCommand =
                            mCharacterCommandFactory.CreateGetAllCharactersForAccountQuery(tGameName,
                                                                                           vAccountName);
                        Caller.LoadCharacters(vCommand.Result);
                    });
        }
    }
}