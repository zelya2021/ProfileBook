using ProfileBook.Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfileBook.Services.Authentication
{
    class Authentication: IAuthentication
    {
        private readonly IRepositoryForUser _repositoryForUser;
        private readonly ISettingsManager _settingsManager;
        public Authentication(IRepositoryForUser repositoryForUser, ISettingsManager settingsManager)
        {
            _repositoryForUser = repositoryForUser;
            _settingsManager = settingsManager;
        }
        public bool UniqueLogin(string login)
        {
            var user = _repositoryForUser.GetItems().FirstOrDefault(u => u.Login == login);
            if (user == null) return true;
            else return false;
        }
        public bool IsUserSignIn(string login, string password)
        {
            var user = _repositoryForUser.GetItems().FirstOrDefault(u => u.Login == login && u.Password == password);
            if (user == null) return false;
            else
            {
                _settingsManager.Id = user.Id;
                return true;
            }
        }
    }
}
