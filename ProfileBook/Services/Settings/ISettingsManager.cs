
namespace ProfileBook.Services.Settings
{
    public interface ISettingsManager
    {
        int Id { get; set; }
        void ClearData();
    }
}
