using System;
using Xamarin.Forms;
using System.IO;
using ProfileBook.Services.Database;
using ProfileBook.iOS.Database;

[assembly: Dependency(typeof(IosDbPath))]
namespace ProfileBook.iOS.Database
{
    class IosDbPath : IPath
    {
        public string GetDatabasePath(string sqliteFilename)
        {
            // определяем путь к бд
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", sqliteFilename);
        }
    }
}