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

        public bool IsAuthorized
        {
            get => _settingsManager.Id >= 0;
        }

        public int GetCurrentUserId()
        {
           return _settingsManager.Id;
        }
    }
}
