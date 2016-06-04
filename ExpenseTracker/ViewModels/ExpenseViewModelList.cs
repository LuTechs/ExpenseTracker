using System.Collections.Generic;

namespace ExpenseTracker.ViewModels
{
    public class ExpenseViewModelList
    {
        public int PageCount { get; set; }
        public int TotalItems { get; set; }
        public ICollection<ExpenseViewModel> Expenses { get; set; }
    }
}