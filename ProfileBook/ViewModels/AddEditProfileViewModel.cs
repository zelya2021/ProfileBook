
using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;
using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class AddEditProfileViewModel : BindableBase, INavigationAware
    {
        private string _title, _name, _nickName, _description, _image;
        bool IsProfileToUpdate = false;
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
        private ProfileModel Profile;
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
            get 
            {
                if (_image == null)
                    return "ic_user.png";
                else
                    return _image; 
            }
            set { SetProperty(ref _image, value); }
        }
        public ICommand ClickOnSaveCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (String.IsNullOrEmpty(NickName) || String.IsNullOrEmpty(Name))
                        await _dialogService.DisplayAlertAsync("Warning!", "The fields must not be empty!", "OK");
                    else
                    {
                        if (IsProfileToUpdate)
                        {
                            //update profile
                            Profile.Image = Image;
                            Profile.Name = Name;
                            Profile.Description = Description;
                            Profile.NickName = NickName;
                            _repositoryForProfile.SaveItem(Profile);
                            IsProfileToUpdate = false;
                            await _navigationService.NavigateAsync("MainList");
                        }
                        else
                        {
                            //add profile
                            _repositoryForProfile.SaveItem(new ProfileModel
                            {
                                Name = Name,
                                Description = Description,
                                Image = Image,
                                Date = DateTime.Now,
                                UserId = _settingsManager.Id,
                                NickName = NickName
                            });

                            await _navigationService.NavigateAsync("MainList");
                        }
                    }
                });
            }

        }
        public ICommand ClickImageCommand
        {
            get
            {
                return new Command(() =>
                {
                    UserDialogs.Instance.ActionSheet(new ActionSheetConfig()
                           .SetTitle("Choose from")
                           .Add("camera", TakePhotoWithCamera, "camera.png")
                           .Add("gallery", FromGallery, "gallery.png")
                       );
                });
            }

        }
        private async void TakePhotoWithCamera()
        {
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    Directory = "Sample",
                    Name = $"{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.jpg"
                });

                if (file == null)
                    return;

                Image = file.Path;
            }
        }
        private async void FromGallery()
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
                Image = photo.Path;
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Profile = parameters.GetValue<ProfileModel>("profileData");
            Image = Profile.Image;
            Name = Profile.Name;
            NickName = Profile.NickName;
            Description = Profile.Description;
            IsProfileToUpdate = true;
        }
    }
}
