
using ProfileBook.Models;
using ProfileBook.Services.Database;
using ProfileBook.Views;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProfileBook
{
    public partial class App : Application
    {
        public const string DBFILENAME = "users_app.db";
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new SignUpView());

            string dbPath = DependencyService.Get<IPath>().GetDatabasePath(DBFILENAME);
            using (var db = new AppContex(dbPath))
            {
                // Создаем бд, если она отсутствует
                db.Database.EnsureCreated();
                if (db.Users.Count() == 0)
                {
                    db.Users.Add(new User { Name = "Tom", NickName = "tommy002", Image = "pic_profile.png", Date = new DateTime(2010, 1, 7), Password="123456"});
                    db.Users.Add(new User { Name = "Alice", NickName = "alice_bee", Image = "pic_profile.png", Date = new DateTime(2020, 5, 4), Password = "789456" });
                    db.SaveChanges();
                }
            }
           
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
