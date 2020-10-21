
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
        public DateTime DateLabel { get; set; }
        public int ProfileID { get; set; }
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
                        //added profile
                        if (String.IsNullOrEmpty(NickName) || String.IsNullOrEmpty(Name))
                            await _dialogService.DisplayAlertAsync("Внимание", "Поля не должны быть пустыми!", "OK");
                        else
                        {
                            DateLabel = new DateTime();
                            DateLabel = DateTime.Now;
                            _repositoryForProfile.SaveItem(new ProfileModel
                            {
                                Id=ProfileID,
                                Name = Name,
                                Description = Description,
                                Image = Image,
                                Date = DateLabel,
                                UserId = _settingsManager.Id,
                                NickName = NickName
                            });

                            await _navigationService.NavigateAsync("MainList");
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
                           .SetTitle("Выбрать с помощью")
                           .Add("Камеры", TakePhotoWithCamera, "camera.png")
                           .Add("Галереи", FromGallery, "gallery.png")
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
            var profile = parameters.GetValue<ProfileModel>("profileData");
            Image = profile.Image;
            Name = profile.Name;
            NickName = profile.NickName;
            Description = profile.Description;
        }
    }
}
