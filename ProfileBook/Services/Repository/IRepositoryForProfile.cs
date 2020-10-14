using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.Repository
{
    public interface IRepositoryForProfile
    {
        IEnumerable<ProfileModel> GetItems();
        ProfileModel GetItem(int id);
        int DeleteItem(int id);
        int SaveItem(ProfileModel item);
    }
}
