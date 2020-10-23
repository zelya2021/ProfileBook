using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SettingsViewViewModel : BindableBase, INavigationAware
    {
        private string _title;
        private bool _isRbSortedByNameChecked, _isRbSortedByNickNameChecked, _isRbSortedByDateChecked;
        private ObservableCollection<ProfileModel> _profileData;
        private readonly INavigationService _navigationService;
        private readonly ISettingsManager _settingsManager;
        public SettingsViewViewModel(INavigationService navigationService, ISettingsManager settingsManager)
        {
            Title = "Settings";
            _navigationService = navigationService;
            _settingsManager = settingsManager;
        }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public bool IsRbSortedByNameChecked
        {
            get { return _isRbSortedByNameChecked; }
            set { SetProperty(ref _isRbSortedByNameChecked, value);}
        }
        public bool IsRbSortedByNickNameChecked
        {
            get { return _isRbSortedByNickNameChecked; }
            set { SetProperty(ref _isRbSortedByNickNameChecked, value); }
        }
        public bool IsRbSortedByDateChecked
        {
            get { return _isRbSortedByDateChecked; }
            set { SetProperty(ref _isRbSortedByDateChecked, value); }
        }
        public ObservableCollection<ProfileModel> ProfileData
        {
            get { return _profileData; }
            set { SetProperty(ref _profileData, value); }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            ProfileData = parameters.GetValue<ObservableCollection<ProfileModel>>("profileModels");
        }
        public ICommand NavigateToMainListCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (IsRbSortedByNameChecked == true)
                    {
                        var sorted = ProfileData.OrderBy(p => p.Name).ToList();
                        ProfileData = new ObservableCollection<ProfileModel>(sorted);

                        var param = new NavigationParameters();
                        param.Add("sortedProfilesByName", ProfileData);

                        await _navigationService.NavigateAsync("MainList", param);
                    }
                });
            }
        }
    }
}
