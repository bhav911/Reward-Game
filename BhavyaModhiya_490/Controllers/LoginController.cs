using BhavyaModhiya_490.CommonWebAPI;
using BhavyaModhiya_490.Session;
using BhavyaModhiya_490_Models.CustomModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BhavyaModhiya_490.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult SignUp()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Clear();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                string response = await WebAPIHelper.HttpGetRequestResponse("api/LoginAPI/DoesUserExist?email=" + userModel.Email);
                bool exist = JsonConvert.DeserializeObject<bool>(response);
                if (exist)
                {
                    TempData["error"] = "User Already Exist";
                    return View(userModel);
                }
                else
                {
                    response = await WebAPIHelper.HttpPostRequestResponse("api/LoginAPI/RegisterUser", JsonConvert.SerializeObject(userModel));
                    bool status = JsonConvert.DeserializeObject<bool>(response);
                    if (status)
                    {
                        TempData["Success"] = "User registered Successfully";
                        return RedirectToAction("SignIn");
                    }
                    return View(userModel);
                }
            }
            return View(userModel);
        }

        public ActionResult SignIn()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Clear();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(LoginModel credentials)
        {
            if (ModelState.IsValid)
            {
                string response = await WebAPIHelper.HttpPostRequestResponse("api/LoginAPI/AuthenticateUser", JsonConvert.SerializeObject(credentials));
                if(response == null)
                {
                    TempData["error"] = "Invalid Credentials";
                    return View(credentials);
                }

                UserModel user = JsonConvert.DeserializeObject<UserModel>(response);
                if (user != null)
                {
                    UserSession.UserID = user.UserID;
                    UserSession.Email = user.Email;
                    TempData["Success"] = "Logged In Successfully";
                    var cookie = new HttpCookie("jwt", user.token)
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTime.Now.AddMinutes(1),
                        Domain = "localhost",
                        Path = "/"
                    };

                    Response.Cookies.Add(cookie);
                    return RedirectToAction("Wallet", "Home");
                }
                TempData["error"] = "Invalid Credentials";
                return View(credentials);
            }
            return View(credentials);
        }
    }
}