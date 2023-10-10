using System.Configuration;
using ReminderBot.src.Database;
using ReminderBot.src.Bot;

namespace ReminderBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TelegramBot bot = new TelegramBot();
            bot.StartReceiving();

            while (true)
            {
                var input = Console.ReadLine();
                if (input == "stop") break;
            }
        }
    }
}