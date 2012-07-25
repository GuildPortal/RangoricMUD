using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RangoricMUD.Accounts.Models;
using RangoricMUD.Bootstrappers.Crews;
using RangoricMUD.Web.Commands;

namespace RangoricMUD.Web.Crews
{
    public class WebCrews : BaseCrew
    {
        protected override void StrapOn()
        {
            Add().OfInterface<IWebCommandFactory>().AsFactory();
            Add()
                .OfInterface<ISendEmailCommand<SendConfirmationModel>>()
                .WithImplementation<SendEmailCommand<SendConfirmationModel>>();
        }
    }
}