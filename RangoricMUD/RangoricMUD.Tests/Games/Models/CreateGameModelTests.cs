using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RangoricMUD.Games.Models;
using RangoricMUD.Web;

namespace RangoricMUD.Tests.Games.Models
{
    [TestFixture]
    public class CreateGameModelTests
    {
        [TestCase("ABC")]
        [TestCase("ABCD")]
        public void GameIsValid(string tName)
        {
            var vCreateGameModel = new CreateGameModel()
            {
                Name = tName,
            };

            Assert.IsTrue(ModelValidator.IsValid(vCreateGameModel));
        }
        [TestCase("")]
        [TestCase("A")]
        [TestCase("AB")]
        public void GameIsInvalid(string tName)
        {
            var vCreateGameModel = new CreateGameModel()
                                       {
                                           Name = tName,
                                       };

            Assert.IsFalse(ModelValidator.IsValid(vCreateGameModel));
        }
    }
}
