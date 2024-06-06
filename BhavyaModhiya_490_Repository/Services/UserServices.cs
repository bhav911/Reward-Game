using BhavyaModhiya_490_Models.Context;
using BhavyaModhiya_490_Models.CustomModels;
using BhavyaModhiya_490_Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_Repository.Services
{
    public class UserServices : IUserInterface
    {
        RewardGame_490Entities _dbContext = new RewardGame_490Entities();

        public bool DoesUserAlreadyExist(string email)
        {
            Users user = _dbContext.Users.FirstOrDefault(q => q.email == email);
            return user != null;
        }

        public bool RegisterUser(Users user)
        {
            try
            {
                Users addedUser = _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                Wallet userWallet = new Wallet()
                {
                    balance = 100,
                    userID = addedUser.userID,
                    chancesLeft = 3
                };
                Wallet addedWallet = _dbContext.Wallet.Add(userWallet);
                _dbContext.SaveChanges();
                Transactions transaction = new Transactions()
                {
                    walletID = addedWallet.walletID,
                    closingAmount = 100,
                    transactionAmount = 100,
                    transactionType = "C",
                    transactionTime = System.DateTime.Now
                };
                _dbContext.Transactions.Add(transaction);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public Users AuthenticateUser(LoginModel credentials)
        {
            Users user = _dbContext.Users.FirstOrDefault(q => q.email == credentials.Email && q.password == credentials.Password);
            return user;
        }
    }
}
