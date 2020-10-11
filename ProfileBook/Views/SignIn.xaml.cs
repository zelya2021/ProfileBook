using ProfileBook.Services.DataBase;
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
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
        }

        private void loginEntry_Completed(object sender, System.EventArgs e)
        {
            nickName = ((Entry)sender).Text;
        }

        private void passwordEntry_Completed(object sender, System.EventArgs e)
        {
            password = ((Entry)sender).Text;
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            using (AppContex db = new AppContex(dbPath))
            {
                var tryUser = db.Users.FirstOrDefault(u => u.NickName == nickName && u.Password == password);
                if (tryUser != null)
                {
                  // Navigation.PushAsync(new MainListView());
                }
                else
                {
                    ;
                }
            }
            // this.Navigation.PopAsync();
        }
    }
}
