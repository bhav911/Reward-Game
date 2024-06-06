using BhavyaModhiya_490_Helpers.ModelConverters;
using BhavyaModhiya_490_Models.Context;
using BhavyaModhiya_490_Models.CustomModels;
using BhavyaModhiya_490_Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace BhavyaModhiya_490_WebAPI.Controllers
{
    public class LoginApiController : ApiController
    {
        UserServices _user = new UserServices();


        [HttpGet]
        [Route("api/LoginAPI/DoesUserExist")]
        public bool DoesUserExist(string email)
        {
            bool doesExist = _user.DoesUserAlreadyExist(email);
            return doesExist;
        }

        [HttpPost]
        [Route("api/LoginAPI/RegisterUser")]
        public bool RegisterUser(UserModel userModel)
        {
            Users user = UserConverter.ConvertUserModelToUser(userModel);
            bool status = _user.RegisterUser(user);
            return status;
        }

        
        [HttpPost]
        [Route("api/LoginAPI/AuthenticateUser")]
        public UserModel AuthenticateUser(LoginModel credentials)
        {
            Users user = _user.AuthenticateUser(credentials);
            UserModel userModel = null;
            if (user != null)
                userModel = UserConverter.ConvertUserToUserModel(user);
            return userModel;
        }
    }
}