using Prism;
using Prism.Ioc;
using ProfileBook.ViewModels;
using ProfileBook.Views;
using Xamarin.Forms;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Authorization;
using ProfileBook.Services;
using ProfileBook.Services.Settings;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using ProfileBook.Services.Authentication;
using Prism.DryIoc;

namespace ProfileBook
{
    public partial class App : PrismApplication
    {

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }
        private IAuthorizationService _authorizationService;
        private IAuthorizationService AuthorizationService =>
            _authorizationService ??= Container.Resolve<IAuthorizationService>();

        protected override async void OnInitialized()
        {
            InitializeComponent();

            if (AuthorizationService.IsAuthorized)
                await NavigationService.NavigateAsync("NavigationPage/MainList");
            else await NavigationService.NavigateAsync("NavigationPage/SignIn");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //packages
            containerRegistry.RegisterInstance<ISettings>(CrossSettings.Current);

            //servises
            containerRegistry.RegisterInstance<IRepositoryForProfile>(Container.Resolve<RepositoryForProfile>());
            containerRegistry.RegisterInstance<IRepositoryForUser>(Container.Resolve<RepositoryForUser>());
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            containerRegistry.RegisterInstance<IAuthentication>(Container.Resolve<Authentication>());


            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignIn, SignInViewModel>();
            containerRegistry.RegisterForNavigation<MainList, MainListViewModel>();
            containerRegistry.RegisterForNavigation<SignUp, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfile, AddEditProfileViewModel>();
            containerRegistry.RegisterForNavigation<ProfileImage, ProfileImageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewViewModel>();
        }
    }
}
