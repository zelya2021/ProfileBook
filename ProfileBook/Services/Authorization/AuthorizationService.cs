using ProfileBook.Models;
using ProfileBook.Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfileBook.Services.Authorization
{
   public class AuthorizationService : IAuthorizationService
    {
        private readonly ISettingsManager _settingsManager;
        private readonly IRepositoryForUser _repositoryForUser;

        AuthorizationService(ISettingsManager settingsManager, IRepositoryForUser repositoryForUser)
        {
            _settingsManager = settingsManager;
            _repositoryForUser = repositoryForUser;
        }
        public bool CurrenrUserId(string login, string password)
        {
            var user = _repositoryForUser.GetItems().FirstOrDefault(u => u.NickName == login && u.Password == password);
            if (user == null) return false;
            else
            {
                _settingsManager.Id = user.Id;
                return true;
            }
        }
    }
}
