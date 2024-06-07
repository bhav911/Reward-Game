using BhavyaModhiya_490.CustomFilters;
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

namespace BhavyaModhiya_490_WebAPI.Controllers
{
    public class HomeApiController : ApiController
    {

        WalletServices _wallet = new WalletServices();

        [HttpGet]
        [Route("api/HomeAPI/GetWalletDetails")]
        public WalletModel GetWalletDetails(int userID, int currentPage = 1)
        {
            Wallet wallet = _wallet.GetWalletDetails(userID);
            WalletModel walletModel = WalletConverter.ConvertWalletToWalletModel(wallet, currentPage);
            walletModel.TodaysEarning = _wallet.GetTodaysEarning(userID);
            return walletModel;
        }

        [HttpGet]
        [Route("api/HomeAPI/IsAmountCrossingLimit")]
        public bool IsAmountCrossingLimit(int userID, int amount)
        {
            bool isAmountCrossed = _wallet.checkLimit(userID, amount);
            if (isAmountCrossed)
            {
                return false;
            }
            bool SaveAmountEarned = _wallet.SaveAmountEarned(userID, amount);
            return true;
        }

        [HttpGet]
        [Route("api/HomeAPI/CheckIfChancesAreLeft")]
        public bool CheckIfChancesAreLeft(int userID)
        {
            bool areChancesLeft = _wallet.checkChances(userID);            
            return areChancesLeft;
        }

        [HttpGet]
        [Route("api/HomeAPI/BuyChances")]
        public int BuyChances(int userID)
        {
            int status = _wallet.BuyChances(userID);
            return status;
        }
    }
}