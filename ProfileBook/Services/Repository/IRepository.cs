using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Services
{
    public interface IRepository
    {
        IEnumerable<UserModel> GetItems();
        UserModel GetItem(int id);
        int DeleteItem(int id);
        int SaveItem(UserModel item);

    }
}
