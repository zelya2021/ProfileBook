using Prism;
using Prism.Ioc;
using ProfileBook.ViewModels;
using ProfileBook.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using ProfileBook.Services.DataBase;
using System.Linq;
using ProfileBook.Models;
using System;
using ProfileBook.Services.Repository;

namespace ProfileBook
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
            string dbPath = DependencyService.Get<IPath>().GetDatabasePath(Repository.DBFILENAME);
            using (var db = new AppContex(dbPath))
            {
                // Создаем бд, если она отсутствует
                db.Database.EnsureCreated();
                if (db.Users.Count() == 0)
                {
                    db.Users.Add(new User { Name = "Tom", NickName = "tommy002", Image = "pic_profile.png", Date = new DateTime(2010, 1, 7), Password = "123456", Description = "some1" });
                    db.Users.Add(new User { Name = "Alice", NickName = "alice_bee", Image = "pic_profile.png", Date = new DateTime(2020, 5, 4), Password = "789456", Description = "some2" });
                    db.SaveChanges();
                }
            }
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/SignIn");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignIn, SignInViewModel>();
            containerRegistry.RegisterForNavigation<MainList, MainListViewModel>();
            containerRegistry.RegisterForNavigation<SignUp, SignUpViewModel>();
        }
    }
}
