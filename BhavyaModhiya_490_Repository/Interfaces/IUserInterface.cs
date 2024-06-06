using BhavyaModhiya_490_Models.Context;
using BhavyaModhiya_490_Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_Repository.Interfaces
{
    public interface IUserInterface
    {
        bool DoesUserAlreadyExist(string email);
        bool RegisterUser(Users user);
        Users AuthenticateUser(LoginModel credentials);
    }
}
