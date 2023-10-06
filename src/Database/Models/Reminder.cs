using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderBot.src.Database.Models
{
    internal class Reminder
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public string? Text { get; set; }
        public DateTime Datetime { get; set; }
    }
}
