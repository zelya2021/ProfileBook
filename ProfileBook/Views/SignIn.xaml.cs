using System;
using Xamarin.Forms;

namespace ProfileBook.Views
{
    public partial class SignIn : ContentPage
    {
        string nickName, password;
        public SignIn()
        {
            InitializeComponent();
            SignInBtn.IsEnabled = false;
        }

        private void loginEntry_Completed(object sender, System.EventArgs e)
        {
            nickName = ((Entry)sender).Text;
            if (String.IsNullOrEmpty(password) || String.IsNullOrEmpty(nickName)) SignInBtn.IsEnabled = false;
            else SignInBtn.IsEnabled = true;
        }

        private void passwordEntry_Completed(object sender, System.EventArgs e)
        {
            password = ((Entry)sender).Text;
            if (String.IsNullOrEmpty(nickName) || String.IsNullOrEmpty(password)) SignInBtn.IsEnabled = false;
            else SignInBtn.IsEnabled = true;
        }
    }
}
