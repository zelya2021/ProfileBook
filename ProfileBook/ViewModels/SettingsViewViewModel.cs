using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProfileBook.Models;
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
        private readonly IPageDialogService _dialogService;
        public SettingsViewViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            Title = "Settings";
            _navigationService = navigationService;
            _dialogService = dialogService;
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

        public void OnNavigatedFrom(INavigationParameters parameters) { }

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
                    List<ProfileModel> sorted;
                    var param = new NavigationParameters();

                    if (IsRbSortedByNameChecked == true)
                    {
                        sorted = ProfileData.OrderBy(p => p.Name).ToList();
                        ProfileData = new ObservableCollection<ProfileModel>(sorted);
                        param.Add("sortedProfile", ProfileData);

                        await _navigationService.NavigateAsync("MainList", param);
                    }
                    else if (IsRbSortedByNickNameChecked == true)
                    {
                        sorted = ProfileData.OrderBy(p => p.NickName).ToList();
                        ProfileData = new ObservableCollection<ProfileModel>(sorted);
                        param.Add("sortedProfile", ProfileData);

                        await _navigationService.NavigateAsync("MainList", param);
                    }
                    else if (IsRbSortedByDateChecked == true)
                    {
                        sorted = ProfileData.OrderBy(p => p.Date).ToList();
                        ProfileData = new ObservableCollection<ProfileModel>(sorted);
                        param.Add("sortedProfile", ProfileData);

                        await _navigationService.NavigateAsync("MainList", param);
                    }
                    else if (IsRbSortedByDateChecked==false&&IsRbSortedByNameChecked==false
                                 &&IsRbSortedByNickNameChecked==false)
                    {
                        await _dialogService.DisplayAlertAsync("Warning!", "Choose sort!", "OK");
                    }
                });
            }
        }
    }
}
