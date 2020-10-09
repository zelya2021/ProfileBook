using ProfileBook.Models;
using ProfileBook.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProfileBook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpView : ContentPage
    {
        string dbPath, nickName, password, confPassword;

        private void passwordEntry_Completed(object sender, EventArgs e)
        {
            password = ((Entry)sender).Text;
        }

        private void confirmPassword_Completed(object sender, EventArgs e)
        {
            confPassword = ((Entry)sender).Text;
        }

        private void loginEntry_Completed(object sender, EventArgs e)
        {
            nickName = ((Entry)sender).Text;
        }

        public SignUpView()
        {
            InitializeComponent();
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            using (AppContex db = new AppContex(dbPath))
            {
                db.Users.Add(new User { NickName=nickName, Password=password});
                db.SaveChanges();
            }
        }
    }
}