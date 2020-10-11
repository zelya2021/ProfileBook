using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.DataBase
{
    public interface IPath
    {
        string GetDatabasePath(string filename);
    }
}
