using Prism;
using Prism.Ioc;
using ProfileBook.ViewModels;
using ProfileBook.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using System;
using ProfileBook.Services.Repository;
using System.IO;
using SQLite;
using ProfileBook.Services.Authorization;
using ProfileBook.Services;
using ProfileBook.Services.Settings;
using ProfileBook.Models;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using ProfileBook.Services.Authentication;

namespace ProfileBook
{
    public partial class App
    {

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            //if (CrossSettings.Current.GetValueOrDefault("Id", -1) != -1) 
            //    await NavigationService.NavigateAsync("NavigationPage/SignIn");
            //else await NavigationService.NavigateAsync("NavigationPage/MainListView");

            await NavigationService.NavigateAsync("NavigationPage/SignIn");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //packages
            containerRegistry.RegisterInstance<ISettings>(CrossSettings.Current);

            //servises
            containerRegistry.RegisterInstance<IRepositoryForProfile>(Container.Resolve<RepositoryForProfile>());
            containerRegistry.RegisterInstance<IRepositoryForUser>(Container.Resolve<RepositoryForUser>());
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            containerRegistry.RegisterInstance<IAuthentication>(Container.Resolve<Authentication>());


            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignIn, SignInViewModel>();
            containerRegistry.RegisterForNavigation<MainList, MainListViewModel>();
            containerRegistry.RegisterForNavigation<SignUp, SignUpViewModel>();
        }
    }
}
