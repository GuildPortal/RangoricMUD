using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RangoricMUD.Bootstrappers.Crews;
using RangoricMUD.Characters.Commands;
using RangoricMUD.Characters.Controllers;

namespace RangoricMUD.Characters.Crews
{
    public class CharactersCrew : BaseCrew
    {
        protected override void StrapOn()
        {
            Add().OfInterface<CharactersHub>().WithImplementation<CharactersHub>();
            Add().OfInterface<ICharactersCommandFactory>().AsFactory();
            Add().OfInterface<ICreateCharacterCommand>().WithImplementation<CreateCharacterCommand>();
        }
    }
}