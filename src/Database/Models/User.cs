using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderBot.src.Database.Models
{
    internal class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public short TimezoneInMinutes {  get; set; }

        public User(long id, string firstName, string? lastName=null, string? username=null,
            short timezoneInMinutes=0)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            TimezoneInMinutes = timezoneInMinutes;
        }
    }
}
