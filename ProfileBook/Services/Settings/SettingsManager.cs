using Plugin.Settings.Abstractions;

namespace ProfileBook.Services.Settings
{
    public class SettingsManager : ISettingsManager
    {
        private readonly ISettings _settings;
        private string _imageProfile;
        public SettingsManager(ISettings settings)
        {
            _settings = settings;
        }
        public int Id 
        {
            get => _settings.GetValueOrDefault(nameof(Id), -1);
            set => _settings.AddOrUpdateValue(nameof(Id), value);
        }
        public string ImageProfile
        {
            get => _imageProfile;
            set => _imageProfile = value;
        }
        public void ClearData()
        {
            Id = -1;
        }
    }
}
