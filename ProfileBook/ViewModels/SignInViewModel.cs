using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProfileBook.Services.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignInViewModel : BindableBase
    {
        private string _title, _loginEntry, _passwordEntry;
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IAuthorizationService _authorizationService;
        public SignInViewModel(INavigationService navigationService, IPageDialogService dialogService, IAuthorizationService authorizationService)
        {
            Title = "SignIn";
            _navigationService = navigationService;
            _dialogService = dialogService;
            _authorizationService = authorizationService;
            //IsEnabledCommand = new DelegateCommand(Execute);
        }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public string LoginEntry
        {
            get { return _loginEntry; }
            set { SetProperty(ref _loginEntry, value); }
        }
        public string PasswordEntry
        {
            get { return _passwordEntry; }
            set { SetProperty(ref _passwordEntry, value); }
        }
        //public ICommand NavigateTomainListCommand
        //{
        //    get
        //    {
        //        return new Command(async () =>
        //        {
        //            if (_authorizationService.CurrenrUserId(LoginEntry, PasswordEntry))
        //                await _navigationService.NavigateAsync("MainList");
        //            else
        //            {
        //                await _dialogService.DisplayAlertAsync("Ошибка", "Пользователь не найден", "OK");
        //                //LoginEntry = string.Empty;
        //                //PasswordEntry = string.Empty;
        //            }
        //        });
        //    }
        //}
        public ICommand NavigateToSignUpCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigationService.NavigateAsync("SignUp");
                });
            }
        }
    }
}
