using ExpenseTracker.ViewModels;

namespace ExpenseTracker.Core.Repositories
{
    public interface IExpenseRepository
    {
        ExpenseViewModelList GetExpensesWithPaginationResult(int page, int pageSize);
        ExpenseViewModelList GetExpensesWithPaginationResultBySearchText(int page, int pageSize, string searchText);

        bool UpdateExpense(int id, ExpenseViewModel expenseViewModel);
        bool DeleteExpense(int id);
        bool AddExpense(ExpenseViewModel expenseViewModel);
    }
}