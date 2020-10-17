using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfileBook.Services.Authentication
{
    class Authentication: IAuthentication
    {
        private readonly IRepositoryForUser _repositoryForUser;
        public Authentication(IRepositoryForUser repositoryForUser)
        {
            _repositoryForUser = repositoryForUser;
        }
        public bool UniqueLogin(string login)
        {
            var user = _repositoryForUser.GetItems().FirstOrDefault(u => u.NickName == login);
            if (user == null) return true;
            else return false;
        }
    }
}
