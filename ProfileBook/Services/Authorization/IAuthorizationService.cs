using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.Authorization
{
    public interface IAuthorizationService
    {
        bool CurrenrUserId(string login, string password);
    }
}
