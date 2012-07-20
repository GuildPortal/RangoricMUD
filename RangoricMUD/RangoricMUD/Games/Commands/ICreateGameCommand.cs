using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RangoricMUD.Commands;
using RangoricMUD.Games.Data;
using RangoricMUD.Games.Models;
using Raven.Client;

namespace RangoricMUD.Games.Commands
{
    public enum eGameCreationStatus
    {
        Success = 0,
        DuplicateName = 1
    }
    public interface ICreateGameCommand : ICommand<eGameCreationStatus>
    {
    }

    public class CreateGameCommand : BaseCommand<eGameCreationStatus>, ICreateGameCommand
    {
        private readonly CreateGameModel mCreateGameModel;
        private readonly IDocumentStore mDocumentStore;

        public CreateGameCommand(CreateGameModel tCreateGameModel, IDocumentStore tDocumentStore)
        {
            mCreateGameModel = tCreateGameModel;
            mDocumentStore = tDocumentStore;
        }

        public override eGameCreationStatus Execute()
        {
            var vSession = mDocumentStore.OpenSession();
            var vGame = new Game {Name = mCreateGameModel.Name};

            vSession.Store(vGame, vGame.Name);
            vSession.SaveChanges();

            return eGameCreationStatus.Success;
        }
    }
}