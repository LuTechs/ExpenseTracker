using AutoMapper;
using ExpenseTracker.Models;
using ExpenseTracker.ViewModels;

namespace ExpenseTracker.Automapper
{
    public class ExpenseViewModeltoExpenseProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ExpenseViewModel, Expense>();
        }
    }
}