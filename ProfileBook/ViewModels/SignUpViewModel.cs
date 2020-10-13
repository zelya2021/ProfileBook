using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Database;
using ProfileBook.Services.DataBase;
using ProfileBook.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignUpViewModel : BindableBase
    {
        private string _title, dbPath;
        Repository repository;
        public SignUpViewModel()
        {
            Title = "SignUp";
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(Initialization.DBFILENAME);
            repository = new Repository(dbPath);
        }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public ICommand AddUserCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await repository.AddUser(new User { NickName = LoginEntry, Password = PasswordEntry });
                });
            }
        }
        public string LoginEntry { get; set; }
        public string PasswordEntry { get; set; }
        public string ConfirmPasswordEntry { get; set; }
        //public ICommand AddUserCommand => new Command(AddUser);
        //public void AddUser()
        //{
        //    using (AppContex db = new AppContex(dbPath))
        //    {
        //        db.Users.Add();
        //        db.SaveChanges();
        //    }
        //}
    }
}
