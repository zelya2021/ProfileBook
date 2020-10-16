using Prism;
using Prism.Ioc;
using ProfileBook.ViewModels;
using ProfileBook.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using System;
using ProfileBook.Services.Repository;
using System.IO;
using SQLite;
using ProfileBook.Services.Authorization;
using ProfileBook.Services;
using ProfileBook.Services.Settings;
using ProfileBook.Models;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace ProfileBook
{
    public partial class App
    {
        public const string DATABASE_NAME = "users.db";
        public const string DATABASE_NAME1 = "profile.db";
        public static RepositoryForUser repUsers;
        public static RepositoryForProfile repProfile;
        public static SQLiteConnection sql;
        public static RepositoryForUser DbUsers
        {
            get
            {
                if (repUsers == null)
                {
                    repUsers = new RepositoryForUser(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));

                }
                return repUsers;
            }
        }
        public static RepositoryForProfile DbProfile
        {
            get
            {
                if (repProfile == null)
                {
                    repProfile = new RepositoryForProfile(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME1));

                    repProfile.SaveItem(new ProfileModel
                    {
                        Name = "name1",
                        Date = new DateTime(2010, 10, 14),
                        Description = "description1",
                        Image = "pic_profile.png"
                    });
                }
                return repProfile;
            }
        }
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            //containerRegistry.RegisterForNavigation<NavigationPage>();


            //packages
            containerRegistry.RegisterInstance<ISettings>(CrossSettings.Current);

            //servises
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            //containerRegistry.RegisterInstance<IRepositoryForProfile>(Container.Resolve<RepositoryForProfile>());
            //containerRegistry.RegisterInstance<IRepositoryForUser>(Container.Resolve<RepositoryForUser>());

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignIn, SignInViewModel>();
            containerRegistry.RegisterForNavigation<MainList, MainListViewModel>();
            containerRegistry.RegisterForNavigation<SignUp, SignUpViewModel>();
        }
    }
}
