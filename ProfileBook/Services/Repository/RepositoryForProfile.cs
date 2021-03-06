﻿using ProfileBook.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProfileBook.Services.Repository
{
    public class RepositoryForProfile : IRepositoryForProfile
    {
        SQLiteConnection database;
        public RepositoryForProfile()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            database = new SQLiteConnection(Path.Combine(path, "users_db.db"));
            database.CreateTable<ProfileModel>();
        }
        public IEnumerable<ProfileModel> GetItems()
        {
            return database.Table<ProfileModel>().ToList();
        }
        public ProfileModel GetItem(int id)
        {
            return database.Get<ProfileModel>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<ProfileModel>(id);
        }
        public int SaveItem(ProfileModel item)
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
