using MonyCounter.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MonyCounter.Models.Commands
{
    public class GetCostsCommand : Command
    {
        public override string Name => "status";

        public override async Task<Message> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var FirstName = message.From.FirstName;
            var isBot = message.From.IsBot;

            dbOperator.Money ob = new dbOperator.Money();
            List<string> iii = ob.GetAllSpending(chatId.ToString());
            string h = "";
            foreach(string i in iii)
            {
                h = h + i + "\n";
            }

            return await client.SendTextMessageAsync(chatId, h);
        }
    }
}