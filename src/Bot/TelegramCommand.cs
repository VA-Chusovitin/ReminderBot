using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using ReminderBot.src.Database;

namespace ReminderBot.src.Bot
{
    internal abstract class TelegramCommand
    {
        protected static SqlDbContext _db = new SqlDbContext();

        public string Name { get;}
        public Func<string, string> Description { get; }
        public byte MinArgs { get; }
        public byte MaxArgs { get; }


        public TelegramCommand(string name, byte maxArgs = 0, byte minArgs = 0)
        {
            Name = name;
            Description = (langCode) => GetLocalizedText(langCode, Name+"Descr");
            MinArgs = minArgs;
            MaxArgs = maxArgs;
        }

        protected string GetLocalizedText(string langCode, string name)
        {
            return Localization.Comd.ResourceManager
                .GetString(name, CultureInfo.GetCultureInfo(langCode)) ?? "";
        }

        public abstract Task Execute(ITelegramBotClient botClient, Message message, params string[] args);
    }
}
