using BhavyaModhiya_490_Helpers.ModelConverters;
using BhavyaModhiya_490_Models.Context;
using BhavyaModhiya_490_Models.CustomModels;
using BhavyaModhiya_490_Repository.Services;
using BhavyaModhiya_490_WebAPI.JWTauthentication;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public HttpResponseMessage AuthenticateUser(LoginModel credentials)
        {
            Users user = _user.AuthenticateUser(credentials);
            UserModel userModel = null;
            if (user != null)
            {
                userModel = UserConverter.ConvertUserToUserModel(user);
                var username = userModel.Username;
                var jwtToken = Authentication.GenerateJWTAuthetication(userModel.Email, username);
                userModel.token = jwtToken;


                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, userModel);
                CookieHeaderValue cookie = new CookieHeaderValue("jwt", jwtToken)
                {
                    HttpOnly = true,
                    Secure = true,
                    Domain = "localhost",
                    Path = "/"
                };
                response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid credentials");
        }
    }
}