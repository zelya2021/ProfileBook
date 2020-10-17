using System;
using System.Collections.Generic;
using System.IO;
using ProfileBook.Models;
using SQLite;

namespace ProfileBook.Services.Repository
{
    public class RepositoryForUser : IRepositoryForUser
    {
        SQLiteConnection database;
        public RepositoryForUser()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            database = new SQLiteConnection(Path.Combine(path, "users_db.db"));
            database.CreateTable<UserModel>();
        }
        public IEnumerable<UserModel> GetItems()
        {
            return database.Table<UserModel>().ToList();
        }
        public UserModel GetItem(int id)
        {
            return database.Get<UserModel>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<UserModel>(id);
        }
        public int SaveItem(UserModel item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}
