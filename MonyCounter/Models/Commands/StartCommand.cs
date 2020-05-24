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
    public class StartCommand : Command
    {
        public override string Name => "start";

        public override async Task<Message> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            //var messageId = message.MessageId;
            //var LastName = message.From.LastName;
            //var Username = message.From.Username;
            var FirstName = message.From.FirstName;
            var isBot = message.From.IsBot;
            if (!isBot)
            {

                
                dbOperator.User ob = new dbOperator.User(chatId, FirstName);
                ob = ob.CheckUser(ob);
                if (ob.Id != null)
                {
                    return await client.SendTextMessageAsync(chatId, "Привет! " + FirstName + ", что новенького?");
                }
                else
                {
                   
                    var ds = ob.AddUser(ob);
                    return await client.SendTextMessageAsync(chatId, "Привет! " + FirstName + ", начнем? ");
                }

            }
            else
            {
                return await client.SendTextMessageAsync(chatId, "Привет!");
            }


            //TODO: Command logic -_-


        }
    }
}