using BhavyaModhiya_490_Models.Context;
using BhavyaModhiya_490_Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_Helpers.ModelConverters
{
    public class WalletConverter
    {
        public static WalletModel ConvertWalletToWalletModel(Wallet wallet, int currentPage)
        {
            WalletModel walletModel = new WalletModel()
            {
                balance = wallet.balance,
                chancesLeft = wallet.chancesLeft
            };
            List<TransactionModel> transactionModelList = new List<TransactionModel>();
            foreach(Transactions transaction in wallet.Transactions.OrderByDescending(t => t.transactionTime))
            {
                TransactionModel transactionModel = new TransactionModel()
                {
                    transactionTime = transaction.transactionTime,
                    transactionAmount = transaction.transactionAmount,
                    transactionType = transaction.transactionType,
                    closingAmount = transaction.closingAmount
                };

                transactionModelList.Add(transactionModel);
            }

            PaginationModel paginationModel = new PaginationModel();
            paginationModel.GetPages(transactionModelList, currentPage);
            walletModel.pages = paginationModel;

            return walletModel;
        }
    }
}
