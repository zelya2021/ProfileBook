using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using System.Collections.ObjectModel;

namespace ProfileBook.ViewModels
{
    public class MainListViewModel : BindableBase
    {
        private string _title;
        private readonly IRepositoryForProfile _repositoryForProfile;
        private ObservableCollection<ProfileModel> _profileData;
        public ObservableCollection<ProfileModel> ProfileData
        {
            get { return _profileData; }
            set { SetProperty(ref _profileData, value); }
        }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private readonly INavigationService _navigationService;
        public MainListViewModel(INavigationService navigationService, IRepositoryForProfile repositoryForProfile)
        {
            Title = "MainList";
            _navigationService = navigationService;
            _repositoryForProfile = repositoryForProfile;
            //ProfileData = new ObservableCollection<ProfileModel>(_repositoryForProfile.GetItems());
        }
    }
}
