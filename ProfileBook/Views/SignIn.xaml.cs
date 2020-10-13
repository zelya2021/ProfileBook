using Prism.Navigation;
using ProfileBook.Services;
using ProfileBook.Services.Database;
using ProfileBook.Services.DataBase;
using ProfileBook.ViewModels;
using System;
using System.Linq;
using Xamarin.Forms;

namespace ProfileBook.Views
{
    public partial class SignIn : ContentPage
    {
        string dbPath, nickName, password;
        public SignIn()
        {
            InitializeComponent();
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(Initialization.DBFILENAME);
            //SignInBtn.IsEnabled = false;
        }

        private void loginEntry_Completed(object sender, System.EventArgs e)
        {
            //nickName = ((Entry)sender).Text;
            //if (String.IsNullOrEmpty(password)|| String.IsNullOrEmpty(nickName)) SignInBtn.IsEnabled = false;
            //else SignInBtn.IsEnabled = true; 
        }

        private void passwordEntry_Completed(object sender, System.EventArgs e)
        {
            //password = ((Entry)sender).Text;
            //if (String.IsNullOrEmpty(nickName)|| String.IsNullOrEmpty(password)) SignInBtn.IsEnabled = false;
            //else SignInBtn.IsEnabled = true;
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            //if(nickName!=null&&password!=null)
            //{
            //    ((Button)sender).IsEnabled = true;
            //    using (AppContex db = new AppContex(dbPath))
            //    {
            //        var tryUser = db.Users.FirstOrDefault(u => u.NickName == nickName && u.Password == password);
            //        if (tryUser != null)
            //        {
            //            Navigation.PushAsync(new MainList());
                       
            //        }
            //        else
            //        {
                        
            //        }
            //    }
            //}
           
            // this.Navigation.PopAsync();
        }
    }
}
