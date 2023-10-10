using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ReminderBot.src.Bot.Commands
{
    internal class Start : TelegramCommand
    {
        public Start() : base("start") { }

        public override async Task Execute(ITelegramBotClient botClient, Message message, params string[] args)
        {
            await botClient.SendTextMessageAsync(message.Chat, 
                string.Concat(GetLocalizedText(message.From.LanguageCode, Name), $"{message.From.FirstName}"));
        }
    }
}
