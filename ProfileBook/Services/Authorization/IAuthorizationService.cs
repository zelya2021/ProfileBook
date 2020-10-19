using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.Authorization
{
    public interface IAuthorizationService
    {
        bool IsAuthorized { get; }
        int GetCurrentUserId();
    }
}
