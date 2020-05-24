using MonyCounter.Controllers;
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
    public class CategoryCommand : Command
    {
        public override string Name => "category";

        public override async Task<Message> Execute(Message message, TelegramBotClient client)
        {
            List<KeyboardButton> buttons = new List<KeyboardButton>();
            List<List<KeyboardButton>> KeyboardButtons = new List<List<KeyboardButton>>();

            dbOperator.Category Category = new dbOperator.Category();
            List<dbOperator.Category > AllCategory = Category.GetAllCategory();
            foreach(dbOperator.Category category in AllCategory)
            {
                buttons.Add(new KeyboardButton(category.Name));
            }




            KeyboardButtons.Add(buttons);
           

            
            ReplyKeyboardMarkup ui = new ReplyKeyboardMarkup(KeyboardButtons, true);

           return await client.SendTextMessageAsync( message.Chat,"У меня есть такие категории: ", ParseMode.Markdown, true, false,  message.MessageId, ui);
        }
    }
}