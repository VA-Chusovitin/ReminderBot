using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using ReminderBot.src.Database.Models;

namespace ReminderBot.src.Database
{
    internal class SqlDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Reminder> Reminders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["localdb"]
            .ConnectionString;
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
