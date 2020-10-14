using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.Settings
{
    public class SettingsManager : ISettingsManager
    {
        private readonly ISettingsManager _settingsManager;
        public SettingsManager(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }
        public int Id
        {
            get => _settingsManager.Id;
            set => _settingsManager.Id = value;
        }
    }
}
