using Prism;
using Prism.Ioc;
using ProfileBook.ViewModels;
using ProfileBook.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using System.Linq;
using ProfileBook.Models;
using System;
using ProfileBook.Services.Repository;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using ProfileBook.Services.Authorization;
using ProfileBook.Services;
using ProfileBook.Services.Settings;

namespace ProfileBook
{
    public partial class App
    {
        public const string DATABASE_NAME = "users.db";
        public static Repository database;
        public static SQLiteConnection sql;
        public static Repository Database
        {
            get
            {
                if (database == null)
                {
                    database = new Repository(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                    //database.SaveItem(new UserModel
                    //{
                    //    NickName = "masha002",
                    //    Password = "12345678"
                    //});
                }
                return database;
            }
        }
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }
        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/SignIn");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            //containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignIn, SignInViewModel>();
            containerRegistry.RegisterForNavigation<MainList, MainListViewModel>();
            containerRegistry.RegisterForNavigation<SignUp, SignUpViewModel>();
          
        }
    }
}
