using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MonyCounter.Models.Commands
{
    public class HelpCommand : Command
    {
        public override string Name => "help";

        public override async Task<Message> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            


               
           return await client.SendTextMessageAsync(chatId, "Не волнуйся, сейчас разберемся");
            
        }
    }
}