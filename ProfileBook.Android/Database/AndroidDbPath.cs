using System;
using System.IO;
using Xamarin.Forms;
using ProfileBook.Droid.Database;
using ProfileBook.Services.Database;

[assembly: Dependency(typeof(AndroidDbPath))]
namespace ProfileBook.Droid.Database
{
    class AndroidDbPath : IPath
    {
        public string GetDatabasePath(string filename)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), filename);
        }
    }
}