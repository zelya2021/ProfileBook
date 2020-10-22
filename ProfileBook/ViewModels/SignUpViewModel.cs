using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services;
using ProfileBook.Services.Authentication;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignUpViewModel : BindableBase
    {
        private string _title, _loginEntry, _passwordEntry, _confirmPasswordEntry;
        bool _isSignInEnable;
        private readonly INavigationService _navigationService;
        private readonly Prism.Services.IPageDialogService _dialogService;
        private readonly IRepositoryForUser _repositoryForUser;
        private readonly IAuthentication _authentication;
        public SignUpViewModel(INavigationService navigationService, Prism.Services.IPageDialogService dialogService,
            IRepositoryForUser repositoryForUser, IAuthentication authentication)
        {
            Title = "SignUp";
            _navigationService = navigationService;
            _dialogService = dialogService;
            _repositoryForUser = repositoryForUser;
            _authentication = authentication;
        }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public string LoginEntry
        {
            get { return _loginEntry; }
            set
            {
                SetProperty(ref _loginEntry, value);
                IsSignUpEnable = IsActivateSignUp();
            }
        }
        public string PasswordEntry
        {
            get { return _passwordEntry; }
            set
            {
                SetProperty(ref _passwordEntry, value);
                IsSignUpEnable = IsActivateSignUp();
            }
        }
        public bool IsSignUpEnable
        {
            get { return _isSignInEnable; }
            set { SetProperty(ref _isSignInEnable, value); }
        }
        public string ConfirmPasswordEntry
        {
            get { return _confirmPasswordEntry; }
            set
            {
                SetProperty(ref _confirmPasswordEntry, value);
                IsSignUpEnable = IsActivateSignUp();
            }
        }
        public ICommand SignUpCommand => new Command(SignUp);
        public async void SignUp()
        {
            while (true)
            {
                Regex startsWithLetter = new Regex(@"^([a-zA-Z][a-zA-Z0-9' ]{0,49})$");
                MatchCollection matchesForLogin = startsWithLetter.Matches(LoginEntry);

                Regex containsLetterNumber = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,16}$");
                MatchCollection matchesForPassword = containsLetterNumber.Matches(PasswordEntry);

                if (LoginEntry.Length < 4 || LoginEntry.Length > 16)
                {
                    await _dialogService.DisplayAlertAsync("Warning!", "Login must be at least 4 and no more than 16 symbols", "OK");
                    LoginEntry = string.Empty;
                    PasswordEntry = string.Empty;
                    ConfirmPasswordEntry = string.Empty;
                    break;
                }
                else if (matchesForLogin.Count <= 0)
                {
                    await _dialogService.DisplayAlertAsync("Warning!", "slkkd", "OK");
                    LoginEntry = string.Empty;
                    PasswordEntry = string.Empty;
                    ConfirmPasswordEntry = string.Empty;
                    break;
                }
                else if (matchesForPassword.Count <= 0)
                {
                    await _dialogService.DisplayAlertAsync("Warning!", "Password must be at least 8 and no more than 16 symbols and " +
                        "must contain at least one uppercase letter, one lowercase letter and one number.", "OK");
                    PasswordEntry = string.Empty;
                    ConfirmPasswordEntry = string.Empty;
                    break;
                }
                else if (PasswordEntry != ConfirmPasswordEntry)
                {
                    await _dialogService.DisplayAlertAsync("Warning!", "Passwords do not match", "OK");
                    PasswordEntry = string.Empty;
                    ConfirmPasswordEntry = string.Empty;
                    break;
                }
                else if (!_authentication.UniqueLogin(LoginEntry))
                {
                    await _dialogService.DisplayAlertAsync("Warning!", "This user is already registered", "OK");
                    break;
                }
                else
                {
                    _repositoryForUser.SaveItem(new UserModel { Login = LoginEntry, Password = PasswordEntry });
                    var param = new NavigationParameters();
                    param.Add("usersLogin", LoginEntry);

                    await _navigationService.NavigateAsync("SignIn",param);
                    break;
                }
            }

        }
        private bool IsActivateSignUp()
        {
            if (LoginEntry == null || LoginEntry.Contains(" ") || PasswordEntry == null
                || PasswordEntry.Contains(" ")|| ConfirmPasswordEntry == null)
                return false;
            return true;
        }
    }
}
