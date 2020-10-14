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
        AuthorizationService(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }
        public bool CurrenrUserId(string login, string password)
        {
            var user = App.DbUsers.GetItems().FirstOrDefault(u => u.NickName == login && u.Password == password);
            if (user == null) return false;
            else
            {
                _settingsManager.Id = user.Id;
                return true;
            }
        }
    }
}
