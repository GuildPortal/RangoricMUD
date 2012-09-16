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
using System.Web;
using RangoricMUD.Characters.Data;
using RangoricMUD.Characters.Models;
using RangoricMUD.Commands;
using Raven.Abstractions.Exceptions;
using Raven.Client;

#endregion

namespace RangoricMUD.Characters.Commands
{
    public class CreateCharacterCommand : BaseCommand<eCharacterCreationStatus>, ICreateCharacterCommand
    {
        private readonly string mAccountName;
        private readonly IDocumentStore mDocumentStore;
        private readonly CreateCharacterModel mModel;

        public CreateCharacterCommand(IDocumentStore tDocumentStore, CreateCharacterModel tModel, string tAccountName)
        {
            mModel = tModel;
            mAccountName = tAccountName;
            mDocumentStore = tDocumentStore;
        }

        public override eCharacterCreationStatus Execute()
        {
            using (var vSession = mDocumentStore.OpenSession())
            {
                vSession.Advanced.UseOptimisticConcurrency = true;

                var vCharacter = new Character {ListName = mModel.ListName, Name = mModel.Name};

                vSession.Store(vCharacter,
                               "Games/" + mModel.GameName + "/" + mAccountName + "/Characters/" + vCharacter.ListName);

                try
                {
                    vSession.SaveChanges();
                }
                catch (ConcurrencyException)
                {
                    return eCharacterCreationStatus.DuplicateListName;
                }
            }
            return eCharacterCreationStatus.Success;
        }
    }

    public enum eCharacterCreationStatus
    {
        Success,
        DuplicateListName
    }

    public interface ICreateCharacterCommand : ICommand<eCharacterCreationStatus>
    {
    }

    public interface ICharactersCommandFactory
    {
        ICreateCharacterCommand CreateCreateCharacterCommand(CreateCharacterModel tModel, string tAccountName);
    }
}