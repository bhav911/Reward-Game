using BhavyaModhiya_490_Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_Repository.Interfaces
{
    public interface IWalletInterface
    {
        Wallet GetWalletDetails(int userID);

    }
}
