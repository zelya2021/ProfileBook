using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
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
        private readonly INavigationService _navigationService;
        private readonly Prism.Services.IPageDialogService _dialogService;
        public SignUpViewModel(INavigationService navigationService, Prism.Services.IPageDialogService dialogService)
        {
            Title = "SignUp";
            _navigationService = navigationService;
            _dialogService = dialogService;
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(Repository.DBFILENAME);
            repository = new Repository(dbPath);
        }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public ICommand SignUpCommand => new Command(SignUp);
        public string LoginEntry { get; set; }
        public string PasswordEntry { get; set; }
        public string ConfirmPasswordEntry { get; set; }

        //public ICommand AddUserCommand => new Command(AddUser);
        public async void SignUp()
        {
            while (true)
            {
                if (LoginEntry.Length < 4 || LoginEntry.Length > 16)
                {
                    await _dialogService.DisplayAlertAsync("Внимание", "Логин должен быть не менее 4 и не более 16 символов", "OK");
                    break;
                }
                else if (PasswordEntry.Length < 8 || PasswordEntry.Length > 16)
                {
                    await _dialogService.DisplayAlertAsync("Внимание", "Пароль должен быть не менее 8 и не более 16 символов", "OK");
                    break;
                }
                else if (PasswordEntry != ConfirmPasswordEntry)
                {
                    await _dialogService.DisplayAlertAsync("Внимание", "Пароли не совпадают", "OK");
                    break;
                }
                else
                {
                   await repository.AddUser(new User { NickName = LoginEntry, Password = PasswordEntry });
                   await _navigationService.NavigateAsync("SignIn");
                    break;
                }
            }

        }
    }
}
