using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.Authentication
{
    public interface IAuthentication
    {
        bool UniqueLogin(string login);
    }
}
