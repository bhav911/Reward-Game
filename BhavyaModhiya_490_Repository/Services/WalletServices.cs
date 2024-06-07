using BhavyaModhiya_490_Models.Context;
using BhavyaModhiya_490_Models.SqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_Repository.Services
{
    public class WalletServices
    {
        RewardGame_490Entities _dbContext = new RewardGame_490Entities();

        public Wallet GetWalletDetails(int userID)
        {
            try
            {
                Wallet wallet = _dbContext.Wallet.FirstOrDefault(q => q.userID == userID);
                return wallet;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int GetTodaysEarning(int userID)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@userID", userID);
            DataTable result = SqlSPHelper.SqlStoredProcedure("getSum", parameters);
            int sum = 0;
            foreach (DataRow row in result.Rows)
            {
                sum = (int)(row["sum"] == DBNull.Value ? 0 : row["sum"]);
            }
            return sum;
        }

        public bool checkLimit(int userID, int amount)
        {
            try
            {
                Users user = _dbContext.Users.FirstOrDefault(q => q.userID == userID);
                Wallet userWallet = user.Wallet.FirstOrDefault();
                userWallet.chancesLeft--;
                _dbContext.SaveChanges();
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@userID", userID);
                DataTable result = SqlSPHelper.SqlStoredProcedure("getSum", parameters);
                int sum = 0;
                foreach (DataRow row in result.Rows)
                {
                    sum = (int)(row["sum"] == DBNull.Value ? 0 : row["sum"]);
                }
                return sum + amount > 500;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SaveAmountEarned(int userID, int amount)
        {
            try
            {
                Wallet wallet = _dbContext.Wallet.FirstOrDefault(q => q.userID == userID);
                Transactions transactions = new Transactions()
                {
                    transactionAmount = amount,
                    transactionType = "C",
                    transactionTime = System.DateTime.Now,
                    walletID = wallet.walletID,
                    closingAmount = wallet.balance + amount
                };
                _dbContext.Transactions.Add(transactions);
                wallet.balance += amount;
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool checkChances(int userID)
        {
            Wallet wallet = _dbContext.Wallet.FirstOrDefault(q => q.userID == userID);
            return wallet.chancesLeft > 0;
        }

        public int BuyChances(int userID)
        {
            Wallet wallet = _dbContext.Wallet.FirstOrDefault(q => q.userID == userID);
            if(wallet.chancesLeft >= 1)
            {
                return -2;
            }
            if(wallet.balance >= 20)
            {
                Transactions transactions = new Transactions()
                {
                    transactionAmount = 20,
                    transactionType = "D",
                    transactionTime = System.DateTime.Now,
                    closingAmount = wallet.balance - 20,
                    walletID = wallet.walletID
                };
                _dbContext.Transactions.Add(transactions);
                wallet.balance = wallet.balance - 20;
                wallet.chancesLeft++;
                _dbContext.SaveChanges();
                return 1;
            }
            return -1;
        }
    }
}
