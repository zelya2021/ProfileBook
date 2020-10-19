
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class AddEditProfileViewModel : BindableBase
    {
        private string _title, _name, _nickName, _description, _image;
        private readonly INavigationService _navigationService;
        private readonly IRepositoryForProfile _repositoryForProfile;
        private readonly IPageDialogService _dialogService;
        private readonly ISettingsManager _settingsManager;
        public AddEditProfileViewModel(INavigationService navigationService, IRepositoryForProfile repositoryForProfile,
             IPageDialogService dialogService, ISettingsManager settingsManager)
        {
            Title = "AddEditProfile";
            _navigationService = navigationService;
            _repositoryForProfile = repositoryForProfile;
            _dialogService = dialogService;
            _settingsManager = settingsManager;
        }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public string NickName
        {
            get { return _nickName; }
            set { SetProperty(ref _nickName, value); }
        }
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
        public string Image
        {
            get { return _image; }
            set
            {
                if (_image == null)
                    SetProperty(ref _image, "ic_user.png");
                else SetProperty(ref _image, value);
            }
        }
        public ICommand ClickOnSave
        {
            get
            {
                return new Command(async () =>
                {
                    if (String.IsNullOrEmpty(NickName) || String.IsNullOrEmpty(Name))
                        await _dialogService.DisplayAlertAsync("Внимание", "Поля не должны быть пустыми!", "OK");
                    else
                    {
                        _repositoryForProfile.SaveItem(new ProfileModel
                        {
                            Name = Name,
                            Description = Description,
                            Image = "ic_user.png",
                            Date = DateTime.Today,
                            UserId = _settingsManager.Id
                        });
                        await _navigationService.NavigateAsync("MainListView");
                    }
                });
            }
        }
    }
}
