using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProfileBook.ViewModels
{
    public class ProfileImageViewModel : BindableBase
    {
        private string _image;
        private readonly ISettingsManager _settingsManager;
        public ProfileImageViewModel(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
           Image = _settingsManager.ImageProfile;
        }
        public string Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }
    }
}
