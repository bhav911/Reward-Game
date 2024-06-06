using BhavyaModhiya_490.CommonWebAPI;
using BhavyaModhiya_490.CustomFilters;
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
    [CustomAuthenticationFilter]
    public class HomeController : Controller
    {
        public ActionResult Dashboard()
        {
            return View();
        }

        public async Task<ActionResult> Wallet(int pageNumber = 1)
        {
            string response = await WebAPIHelper.HttpGetRequestResponse($"api/HomeAPI/GetWalletDetails?userID={UserSession.UserID}&currentPage={pageNumber}");
            WalletModel wallet = JsonConvert.DeserializeObject<WalletModel>(response);
            return View(wallet);
        }

        public async Task<JsonResult> PlayGame()
        {
            string response = await WebAPIHelper.HttpGetRequestResponse($"api/HomeAPI/CheckIfChancesAreLeft?userID={UserSession.UserID}");
            bool status = JsonConvert.DeserializeObject<bool>(response);
            if (!status)
            {
                return Json(-2, JsonRequestBehavior.AllowGet);
            }

            int AmountEarned = GetRandomNumber();
            response = await WebAPIHelper.HttpGetRequestResponse($"api/HomeAPI/IsAmountCrossingLimit?userID={UserSession.UserID}&amount={AmountEarned}");
            status = JsonConvert.DeserializeObject<bool>(response);
            if (status)
            {
                return Json(AmountEarned, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(-1, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult StartGame()
        {
            return View();
        }

        [NonAction]
        private int GetRandomNumber()
        {
            Random random = new Random();
            int number = random.Next(1, 101);
            return number;
        }

        public async Task<ActionResult> BuyChances()
        {
            string response = await WebAPIHelper.HttpGetRequestResponse($"api/HomeAPI/BuyChances?userID={UserSession.UserID}");
            bool status = JsonConvert.DeserializeObject<bool>(response);
            if (status)
            {
                TempData["success"] = "Bought 1 Chance";

            }
            else
            {
                TempData["error"] = "Does not have enough balance";

            }
            return RedirectToAction("Wallet");
        }

    }
}