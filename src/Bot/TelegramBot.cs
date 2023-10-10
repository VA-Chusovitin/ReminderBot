using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using System.Reflection;
using System.Globalization;

namespace ReminderBot.src.Bot
{
    internal class TelegramBot
    {
        private readonly ITelegramBotClient _bot;
        private readonly Dictionary<string, TelegramCommand> _comands = new Dictionary<string, TelegramCommand>();

        public TelegramBot()
        {
            try
            {
                _bot = new TelegramBotClient(GetTelegramToken());
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("TelegramBot token is missing in the configuration file.");
                Environment.Exit(1);
            }

            Type[] typeList = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => String.Equals(t.BaseType, Type.GetType("ReminderBot.src.Bot.TelegramCommand")))
                .ToArray();
            foreach (Type type in typeList)
            {
                var command = Activator.CreateInstance(type);
                _comands.Add(type.GetProperty("Name").GetValue(command).ToString(), (TelegramCommand)command);
            }
        }

        private string GetTelegramToken()
        {
            return ConfigurationManager.AppSettings["TelegramBotToken"];
        }

        private async Task HandlerUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                var command = message.Text.ToLower().Split(" ")[0].Substring(1);
                var args = message.Text.Split(" ").Skip(1).ToArray();

                if (_comands.ContainsKey(command))
                {
                    await _comands[command].Execute(botClient, message, args);
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat, 
                    string.Concat($"{message.From.FirstName}", Localization.Comd.ResourceManager
                    .GetString("not exist", CultureInfo.GetCultureInfo(message.From.LanguageCode))));
            }
        }

        private async Task HandlerErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }

        public void StartReceiving()
        {
            _bot.StartReceiving(HandlerUpdateAsync, HandlerErrorAsync, 
                new ReceiverOptions(), new CancellationTokenSource().Token);
            Console.WriteLine("Bot is running");
        }
    }
}