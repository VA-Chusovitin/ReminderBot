using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using DB = ReminderBot.src.Database.Models;
using ReminderBot.src.Database;

namespace ReminderBot.src.Bot.Commands
{
    internal class Start : TelegramCommand
    {
        public Start() : base("start") { }

        public override async Task Execute(ITelegramBotClient botClient, Message message, params string[] args)
        {
            User user = message.From;

            if (_db.Users.Where(u => u.Id==user.Id).ToArray().Length==0)
            {
                await _db.Users.AddAsync(new DB.User(user.Id, user.FirstName, user.LastName, user.Username));
                await _db.SaveChangesAsync();
            }
            await botClient.SendTextMessageAsync(message.Chat, 
                string.Concat(GetLocalizedText(user.LanguageCode, Name), $"{user.FirstName}"));
        }
    }
}
