using MonyCounter.Controllers;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace MonyCounter.Models.Commands
{
    public class AddCommand : Command
    {
        public override string Name => "text";

        dbOperator.Money money = new dbOperator.Money();
        public override async Task<Message> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            


            //List<KeyboardButton> buttons = new List<KeyboardButton>();
            //buttons.Add(new KeyboardButton("Расход"));
            //buttons.Add(new KeyboardButton("Доход"));

            //ReplyKeyboardMarkup ui = new ReplyKeyboardMarkup(buttons, true);
            

            money.idUser = chatId.ToString();
           
           
            if(message.Text.IndexOf('+')!= -1)
            {
                money.isCosts = 0;

                string[] parts = message.Text.Split(new Char[] { '+' });
                money.spending = parts[1]; //Получаем первый элемент
                money.nameCost = parts[0].ToLower().Trim(); //получаем второй элемент



            }
            else if(message.Text.IndexOf('-') != -1)
            {
                money.isCosts = 1;
                string[] parts = message.Text.Split(new Char[] { '-' });
                money.spending = parts[1]; //Получаем первый элемент
                money.nameCost = parts[0].ToLower().Trim(); //получаем второй элемент
            }
            
            string res = money.addCost(money);

            // return await client.SendTextMessageAsync(chatId, "Отлично, я все записал!" , ParseMode.Markdown, true, false, message.MessageId);


            if (res == "Good")
            {
                return await client.SendTextMessageAsync(chatId, "Я все записал! Возвращайся скорее!", ParseMode.Markdown, true, false, message.MessageId);
            }
            else
            {
                return await client.SendTextMessageAsync(chatId, "Кажется что то пошло не так, попробуй еще раз!", ParseMode.Markdown, true, false, message.MessageId);
            }



        }

        //public async Task<Message> Add(Message message, TelegramBotClient client)
        //{
        //    var chatId = message.Chat.Id;
        //    var messageId = message.MessageId;


        //    // in BD



        //    if (message.Text == "Расход")
        //    {
        //        money.isCosts = 1;
        //    }
        //    else if (message.Text == "Доход")
        //    {
        //        money.isCosts = 0;
        //    }
        //    money.idUser = chatId.ToString();

        //    string res =  money.UpdateIsCost(money);

        //    if(res == "Good")
        //    {
        //        return await client.SendTextMessageAsync(chatId, "Я все записал! Возвращайся скорее!");
        //    }
        //    else
        //    {
        //        return await client.SendTextMessageAsync(chatId, "Кажется что то пошло не так, попробуй еще раз!");
        //    }


        //}
    }
}