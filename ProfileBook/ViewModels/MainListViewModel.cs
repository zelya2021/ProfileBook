using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using System.Collections.ObjectModel;
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
        public MainListViewModel(INavigationService navigationService, IRepositoryForProfile repositoryForProfile)
        {
            Title = "MainList";
            _navigationService = navigationService;
            _repositoryForProfile = repositoryForProfile;
            ProfileData = new ObservableCollection<ProfileModel>(_repositoryForProfile.GetItems());
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
    }
}
