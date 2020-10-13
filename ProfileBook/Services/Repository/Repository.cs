using Microsoft.EntityFrameworkCore;
using ProfileBook.Models;
using ProfileBook.Services.DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Services.Repository
{
    public class Repository : IRepository
    {
        public const string DBFILENAME = "users_app.db";
        private readonly AppContex appContext;
        public Repository(string dbPath)
        {
            appContext = new AppContex(dbPath);
        }
        public async Task<bool> AddUser(User user)
        {
            try
            {
                var tracking = await appContext.Users.AddAsync(user);
                await appContext.SaveChangesAsync();
                return tracking.State == EntityState.Added;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public AppContex GetContext()
        {
            return new AppContex(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "userDB"));
        }

        public async Task<User> GetUserById(int id)
        {
            try
            {
                return await appContext.Users.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                return await appContext.Users.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<User>> QueryUser(Func<User, bool> predicate)
        {
            try
            {
                return appContext.Users.Where(predicate).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateUser(User user)
        {
            try
            {
                var tracking = appContext.Users.Update(user);
                await appContext.SaveChangesAsync();
                return tracking.State == EntityState.Modified;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
