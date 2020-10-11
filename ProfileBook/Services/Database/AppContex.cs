using Microsoft.EntityFrameworkCore;
using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.DataBase
{
    public class AppContex : DbContext
    {
        private string _databasePath;

        public DbSet<User> Users { get; set; }

        public AppContex(string databasePath)
        {
            _databasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
    }
}
