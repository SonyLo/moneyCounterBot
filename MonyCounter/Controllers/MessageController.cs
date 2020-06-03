using MonyCounter.Models;
using MonyCounter.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Telegram.Bot.Types;

namespace MonyCounter.Controllers
{
    public class MessageController : ApiController
    {
        [Route(@"api/message/update")] //webhook uri part
        public async Task<OkResult> Update([FromBody]Update update)
        {
            var commands = Bot.Commands;
            var message = update.Message;
            var client = await Bot.Get();
          

            foreach (var command in commands)
            {
                if (command.Contains(message.Text))
                {
                    await command.Execute(message, client);
                    return Ok();

                }
               

            }

            
            AddCommand addCommand = new AddCommand();
            if (message.Text.IndexOf('-') != -1 || message.Text.IndexOf('+') != -1)
            {
                //вызвать addCommand
                await addCommand.Execute(message, client);
               
            }
            //else
            //{
            //    await addCommand.Add(message, client);
               
            //}

            return Ok();
        }
    }
}
