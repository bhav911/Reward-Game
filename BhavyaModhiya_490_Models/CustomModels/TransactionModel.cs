using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_Models.CustomModels
{
    public class TransactionModel
    {
        public int transactionID { get; set; }
        public Nullable<int> walletID { get; set; }
        public Nullable<decimal> transactionAmount { get; set; }
        public string transactionType { get; set; }
        public Nullable<decimal> closingAmount { get; set; }
        public Nullable<System.DateTime> transactionTime { get; set; }
    }
}
