using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class MainListViewModel : BindableBase
    {
        private string _title;
        public ObservableCollection<ProfileModel> ProfileData { get; set; }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private readonly INavigationService _navigationService;
        private readonly IRepositoryForProfile _repositoryForProfile;
        private readonly ISettingsManager _settingsManager;
        public ICommand RemoveCommand => new Command<ProfileModel>(Remove);
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
        private void Remove(object profileObj)
        {
            ProfileModel profile = profileObj as ProfileModel;
            if (profile == null) return;

            _repositoryForProfile.DeleteItem(profile.Id);
            ProfileData = new ObservableCollection<ProfileModel>(_repositoryForProfile.GetItems().Where(u => u.UserId == _settingsManager.Id));
        }
    }
}
