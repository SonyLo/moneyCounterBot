using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MonyCounter.Models.Commands
{
    public class EditCommand : Command
    {
        public override string Name => "edit";

        public override Task<Message> Execute(Message message, TelegramBotClient client)
        {
            throw new NotImplementedException();
        }
    }
}