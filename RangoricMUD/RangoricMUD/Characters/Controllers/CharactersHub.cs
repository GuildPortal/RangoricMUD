using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using RangoricMUD.Characters.Commands;
using RangoricMUD.Characters.Models;
using RangoricMUD.Security;
using SignalR.Hubs;

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
            return Task.Factory.StartNew(() =>
                                             {
                                                 var vAccountName = mSignInPersistance.AccountName(Context.ConnectionId);
                                                 var vCommand =
                                                     mCharacterCommandFactory.CreateCreateCharacterCommand(tModel, vAccountName);
                                                 return vCommand.Execute() == eCharacterCreationStatus.Success;
                                             });
        }
        public void LoadAll(string tGameName)
        {
            Task.Factory.StartNew(() =>
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