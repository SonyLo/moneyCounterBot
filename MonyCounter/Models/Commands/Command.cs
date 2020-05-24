
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MonyCounter.Models.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public abstract Task<Telegram.Bot.Types.Message> Execute(Message message, TelegramBotClient client);

        public bool Contains(string command)
        {
            return command.Contains(this.Name);//&& command.Contains(AppSettings.Name)
        }

        
    }
}