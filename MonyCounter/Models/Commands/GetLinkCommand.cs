using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace MonyCounter.Models.Commands
{
    public class GetLinkCommand : Command
    {
        public override string Name => "link";

        public override async Task<Message> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            InlineKeyboardButton inline = new InlineKeyboardButton();
            inline.Text = "Жамкни";
            inline.Url = "https://money-counter-app.azurewebsites.net/User/getuser?id=" + chatId;

            
            InlineKeyboardMarkup replyKeyboardMarkup = new InlineKeyboardMarkup(inline);


            


            return await client.SendTextMessageAsync(message.Chat, "Вот твоя ссылка ", ParseMode.Markdown, true, false, message.MessageId, replyKeyboardMarkup);
        }
    }
}