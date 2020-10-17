using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProfileBook.Services.Authorization;
using ProfileBook.Services.Settings;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignInViewModel : BindableBase
    {
        private string _title, _loginEntry, _passwordEntry;
        private readonly INavigationService _navigationService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IPageDialogService _dialogService;

        public SignInViewModel(INavigationService navigationService, IPageDialogService dialogService, 
            IAuthorizationService authorizationService)
        {
            Title = "SignIn";
            _navigationService = navigationService;
            _authorizationService = authorizationService;
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
                    if (_authorizationService.CurrenrUserId(LoginEntry, PasswordEntry))
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
    }
}
