using ProfileBook.Models;
using ProfileBook.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProfileBook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInView : ContentPage
    {
        string dbPath, nickName, password;
        public SignInView()
        {
            InitializeComponent();
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
        }
       
        private void FindUser(object sender, EventArgs e)
        {
            using (AppContex db = new AppContex(dbPath))
            {
                var tryUser = db.Users.FirstOrDefault(u => u.NickName == nickName && u.Password == password);
                if (tryUser != null)
                {
                    Navigation.PushAsync(new MainListView());
                }
                else
                {
                    ;
                }
            }
           // this.Navigation.PopAsync();
        }

        private void passwordEntry_Completed(object sender, EventArgs e)
        {
            password = ((Entry)sender).Text;
        }

        private void loginEntry_Completed(object sender, EventArgs e)
        {
            nickName = ((Entry)sender).Text;
        }
    }
}