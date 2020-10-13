using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignInViewModel : BindableBase
    {
        private string _title;
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        public SignInViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            Title = "SignIn";
            _navigationService = navigationService;
            _dialogService = dialogService;
            IsEnabledCommand = new DelegateCommand(Execute);
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
