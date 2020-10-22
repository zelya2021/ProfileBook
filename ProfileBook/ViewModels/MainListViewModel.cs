using Acr.UserDialogs;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;
using ProfileBook.Views;
using Rg.Plugins.Popup.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class MainListViewModel : BindableBase
    {
        private string _title;
        private bool _sProfilesExists;
        private ProfileModel _selectedProfile;
        private ObservableCollection<ProfileModel> _profileData;
        public ObservableCollection<ProfileModel> ProfileData 
        {
            get {
                if (_profileData.Count==0)
                    IsProfilesExists = true;
                return _profileData; 
            }
            set { SetProperty(ref _profileData, value);  }
        }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public bool IsProfilesExists
        {
            get { return _sProfilesExists; }
            set  { SetProperty(ref _sProfilesExists, value); }
        }
        public ProfileModel SelectedProfile
        {
            get { return _selectedProfile; }
            set
            {
                SetProperty(ref _selectedProfile, value);
                if(_selectedProfile!=null)
                ProfileClickCommand.Execute(_selectedProfile);
            }
        }

        private readonly INavigationService _navigationService;
        private readonly IRepositoryForProfile _repositoryForProfile;
        private readonly ISettingsManager _settingsManager;
        public ICommand RemoveCommand => new Command<ProfileModel>(Remove);
        public ICommand EditCommand => new Command<ProfileModel>(Edit);
        public ICommand ProfileClickCommand => new Command<ProfileModel>(ProfileClick);
        public MainListViewModel(INavigationService navigationService, IRepositoryForProfile repositoryForProfile,
            ISettingsManager settingsManager)
        {
            Title = "MainList";
            _navigationService = navigationService;
            _repositoryForProfile = repositoryForProfile;
            _settingsManager = settingsManager;
             ProfileData = new ObservableCollection<ProfileModel>(_repositoryForProfile.GetItems().Where(u => u.UserId == _settingsManager.Id));
        }
        public ICommand NavigateToAddEditProfile
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigationService.NavigateAsync("AddEditProfile");
                });
            }
        }
        public ICommand LogOutOfAccount
        {
            get
            {
                return new Command(async () =>
                {
                    _settingsManager.ClearData();
                    await _navigationService.NavigateAsync("SignIn");
                });
            }
        }
        private async void Remove(object profileObj)
        {
            ProfileModel profile = profileObj as ProfileModel;
            if (profile == null) return;
            var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
            {
                Message = "Do you really want to delete?",
                OkText = "Yes",
                CancelText = "No"
            });
            if (result)
            {
                _repositoryForProfile.DeleteItem(profile.Id);
                ProfileData = new ObservableCollection<ProfileModel>(_repositoryForProfile.GetItems().Where(u => u.UserId == _settingsManager.Id));
            }
        }
        private async void Edit(object profileObj)
        {
            ProfileModel profile = profileObj as ProfileModel;
            if (profile == null) return;

            var param = new NavigationParameters();
            param.Add("profileData", profile);

            await _navigationService.NavigateAsync("AddEditProfile", param);
        }
        private void ProfileClick(ProfileModel model)
        {
            _settingsManager.ImageProfile = model.Image;
            PopupNavigation.Instance.PushAsync(new ProfileImage());
        }
    }
}
