using ExpenseTracker.ViewModels;

namespace ExpenseTracker.Core.Repositories
{
    public interface IExpenseRepository
    {
        ExpenseViewModelList GetExpensesWithPaginationResult(int page, int pageSize);
    }
}