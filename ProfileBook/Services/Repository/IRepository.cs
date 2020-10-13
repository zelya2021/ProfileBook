using ProfileBook.Models;
using ProfileBook.Services.DataBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Services
{
    public interface IRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<bool> AddUser(User user);
        Task<bool> UpdateUser(User user);
        Task<IEnumerable<User>> QueryUser(Func<User,bool> predicate);
        AppContex GetContext();

    }
}
