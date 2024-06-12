using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_Models.CustomModels
{
    public class UserModel
    {
        public int UserID { get; set; }

        [Required]
        [RegularExpression("^[A-Za-z0-9_ ]{5,18}$", ErrorMessage = "Alphanumeric string that may include _ having a length of 3 to 18 characters")]
        public string Username { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage ="Email Format Invalid")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage = "Minimum eight characters, at least one letter, one number and one special character")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage ="Password Does Not Match")]
        public string ConfirmPassword { get; set; }

        public string token { get; set; }
    }
}
