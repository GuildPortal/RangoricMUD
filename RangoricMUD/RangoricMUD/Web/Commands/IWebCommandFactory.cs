using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RangoricMUD.Web.Models;

namespace RangoricMUD.Web.Commands
{
    public interface IWebCommandFactory
    {
        ISendEmailCommand CreateSendEmailCommand(SendEmailModel tSendEmailModel);
    }
}