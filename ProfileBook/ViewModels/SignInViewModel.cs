using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
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
        public SignInViewModel(INavigationService navigationService)
        {
            Title = "SignIn";
            _navigationService = navigationService;
            IsEnabledCommand = new DelegateCommand(Execute);
        }
        public string LoginEntry 
        {
            get { return _loginEntry; }
            set
            {
                if(PasswordEntry!=null&& LoginEntry!=null)
                    IsEnabledCommand.RaiseCanExecuteChanged();
                SetProperty(ref _loginEntry, value);
            }
        }
        public string PasswordEntry
        {
            get { return _passwordEntry; }
            set
            {
                if (PasswordEntry != null && LoginEntry != null)
                    IsEnabledCommand.RaiseCanExecuteChanged();
                SetProperty(ref _passwordEntry, value);
            }
        }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public DelegateCommand IsEnabledCommand { get; set; }
        //public ICommand NavigateTomainListCommand
        //{
        //    get
        //    {
        //        return new Command(async () =>
        //        {
        //            await _navigationService.NavigateAsync("MainList");
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
        private async void Execute()
        {
            await _navigationService.NavigateAsync("MainList");
        }
        //private bool CanExecute()
        //{
        //    if (LoginEntry != null && PasswordEntry != null)
        //    {
        //        IsEnabledCommand.RaiseCanExecuteChanged();
        //        return true;
        //    }
        //    return false;
        //}
    }
}
