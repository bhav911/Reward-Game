using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_Models.CustomModels
{
    public  class WalletModel
    {
        public int walletID { get; set; }
        public Nullable<int> userID { get; set; }
        public Nullable<decimal> balance { get; set; }
        public Nullable<short> chancesLeft { get; set; }
        public decimal TodaysEarning { get; set; }
        public PaginationModel pages { get; set; }
    }
}
