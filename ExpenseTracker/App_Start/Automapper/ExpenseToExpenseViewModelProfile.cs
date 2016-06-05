using AutoMapper;
using ExpenseTracker.Models;
using ExpenseTracker.ViewModels;

namespace ExpenseTracker.Automapper
{
    public class ExpenseToExpenseViewModelProfile:Profile
    {
        protected override void Configure()
        {
            CreateMap<Expense, ExpenseViewModel>();
        }
    }
}