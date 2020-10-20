using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class MainListViewModel : BindableBase
    {
        private string _title;
        //private ObservableCollection<ProfileModel> _profileData;
        //public ObservableCollection<ProfileModel> ProfileData
        //{
        //    get { return _profileData; }
        //    set { SetProperty(ref _profileData, value); }
        //}
        public ObservableCollection<ProfileModel> ProfileData { get; set; }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private readonly INavigationService _navigationService;
        private readonly IRepositoryForProfile _repositoryForProfile;
        private readonly ISettingsManager _settingsManager;
        public MainListViewModel(INavigationService navigationService, IRepositoryForProfile repositoryForProfile,
            ISettingsManager settingsManager)
        {
            Title = "MainList";
            _navigationService = navigationService;
            _repositoryForProfile = repositoryForProfile;
            _settingsManager = settingsManager;
             ProfileData = new ObservableCollection<ProfileModel>(_repositoryForProfile.GetItems());
            //ProfileData = new ObservableCollection<ProfileModel>();
            //ProfileData.Add(new ProfileModel { Name = "Vasya", NickName = "vasya022", Image = "ic_user.png", Description = "some description", Date = DateTime.Today });
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
    }
}
