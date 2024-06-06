using BhavyaModhiya_490_Models.Context;
using BhavyaModhiya_490_Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_Helpers.ModelConverters
{
    public class UserConverter
    {
        public static Users ConvertUserModelToUser(UserModel userModel)
        {
            Users user = new Users()
            {
                email = userModel.Email,
                username = userModel.Username,
                password = userModel.Password
            };

            return user;
        }

        public static UserModel ConvertUserToUserModel(Users user)
        {
            UserModel userModel = new UserModel()
            {
               Email = user.email,
               UserID = user.userID,
               Username = user.username
            };

            return userModel;
        }
    }
}
