using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProfileBook.ViewModels
{
    public class SignInViewModel : BindableBase
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private DelegateCommand _navigateToMainList, _navigateToSignUp;
        private readonly INavigationService _navigationService;

        public DelegateCommand NavigateToMainList =>
            _navigateToMainList ?? (_navigateToMainList = new DelegateCommand(ExecuteNavigateToMainList));
        public  DelegateCommand NavigateToSignUp =>
               _navigateToSignUp ?? (_navigateToSignUp = new DelegateCommand(ExecuteNavigateToSignUp));


        public SignInViewModel(INavigationService navigationService)
        {
            Title = "SignIn";
            _navigationService = navigationService;
        }

        async void ExecuteNavigateToMainList()
        {
            await _navigationService.NavigateAsync("MainList");
        }
        async void ExecuteNavigateToSignUp()
        {
            await _navigationService.NavigateAsync("SignUp");
        }
    }
}
