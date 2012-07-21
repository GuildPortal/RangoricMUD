using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RangoricMUD.Games.Models;

namespace RangoricMUD.Games.Commands
{
    public interface IGameCommandFactory
    {
        ICreateGameCommand CreateCreateGameCommand(CreateGameModel tCreateGameModel);
    }
}