using System;
using Xamarin.Forms;

namespace ProfileBook.Views
{
    public partial class SignUp : ContentPage
    {
        string nickName, password, _confirmPassword;
        public SignUp()
        {
            InitializeComponent();
            SignUnBtn.IsEnabled = false;
        }

        private void loginEntry_Completed(object sender, System.EventArgs e)
        {
            nickName = ((Entry)sender).Text;
            if (String.IsNullOrEmpty(password) || String.IsNullOrEmpty(nickName)|| String.IsNullOrEmpty(_confirmPassword)) SignUnBtn.IsEnabled = false;
            else SignUnBtn.IsEnabled = true;
        }

        private void passwordEntry_Completed(object sender, System.EventArgs e)
        {
            password = ((Entry)sender).Text;
            if (String.IsNullOrEmpty(password) || String.IsNullOrEmpty(nickName) || String.IsNullOrEmpty(_confirmPassword)) SignUnBtn.IsEnabled = false;
            else SignUnBtn.IsEnabled = true;
        }

        private void confirmPassword_Completed(object sender, System.EventArgs e)
        {
            _confirmPassword = ((Entry)sender).Text;
            if (String.IsNullOrEmpty(password) || String.IsNullOrEmpty(nickName) || String.IsNullOrEmpty(_confirmPassword)) SignUnBtn.IsEnabled = false;
            else SignUnBtn.IsEnabled = true;
        }
    }
}
