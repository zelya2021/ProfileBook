using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.Database
{
    public interface IPath
    {
        string GetDatabasePath(string filename);
    }
}
