using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace ReminderBot.src.Bot.Commands
{
    internal class Help : TelegramCommand
    {
        public Help() : base("help", 1) { }

        public override async Task Execute(ITelegramBotClient botClient, Message message, params string[] args)
        {
            await botClient.SendTextMessageAsync(message.Chat, GetLocalizedText(message.From.LanguageCode, Name));
        }
    }
}
