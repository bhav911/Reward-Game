using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_Models.CustomModels
{
    public class PaginationModel
    {
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public List<TransactionModel> TransactionModelList { get; set; }


        public void GetPages(List<TransactionModel> transactionModelList, int currentPage)
        {
            int maxPage = 15;
            this.CurrentPage = currentPage;
            this.TransactionModelList = transactionModelList.Skip((currentPage - 1) * maxPage).Take(maxPage).ToList();
            int totalEntries = transactionModelList.Count();
            this.TotalPages = totalEntries / maxPage + 1;
        }
    }

}
