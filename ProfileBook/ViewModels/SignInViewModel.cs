﻿using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProfileBook.Services.Authentication;
using ProfileBook.Services.Authorization;
using ProfileBook.Services.Settings;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignInViewModel : BindableBase, INavigationAware
    {
        private string _title, _loginEntry, _passwordEntry;
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IAuthentication _authentication;

        public SignInViewModel(INavigationService navigationService, IPageDialogService dialogService,
            IAuthentication authentication)
        {
            Title = "SignIn";
            _navigationService = navigationService;
            _authentication = authentication;
            _dialogService = dialogService;
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
        public ICommand NavigateTomainListCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (_authentication.IsUserSignIn(LoginEntry, PasswordEntry))
                        await _navigationService.NavigateAsync("MainList");
                    else
                    {
                        await _dialogService.DisplayAlertAsync("Ошибка", "Пользователь не найден", "OK");
                        LoginEntry = string.Empty;
                        PasswordEntry = string.Empty;
                    }
                });
            }
        }
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

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new System.NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            LoginEntry = parameters.GetValue<string>("usersLogin");
        }
    }
}
