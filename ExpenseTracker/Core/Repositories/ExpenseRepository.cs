using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoMapper.QueryableExtensions;
using ExpenseTracker.Core.Helpers;
using ExpenseTracker.Models;
using ExpenseTracker.ViewModels;

namespace ExpenseTracker.Core.Repositories
{
    public class ExpenseRepository : IExpenseRepository,IDisposable
    {
        private readonly ApplicationDbContext _applicationDbContext;


        public ExpenseRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public ExpenseViewModelList GetExpensesWithPaginationResult(int page, int pageSize)
        {
            var totalItems = _applicationDbContext.Expenses.Count();
            var pageCount = PageCount.Count(totalItems, pageSize);

            var expensePage = _applicationDbContext.Expenses
                .OrderBy(a => a.Id)
                .Skip((page - 1)*pageSize)
                .Take(pageSize)
                .ProjectTo<ExpenseViewModel>()
                .ToList();

            var expenseList = new ExpenseViewModelList
            {
                PageCount = pageCount,
                TotalItems = totalItems,
                Expenses = expensePage
            };

            return expenseList;
        }

        public ExpenseViewModelList GetExpensesWithPaginationResultBySearchText(int page, int pageSize, string searchText)
        {
            var totalItems = _applicationDbContext.Expenses.Count(e => e.Title.ToLower().Contains(searchText.ToLower()));
            var pageCount = PageCount.Count(totalItems, pageSize);

            var expenseList = _applicationDbContext.Expenses
                .Where(e => e.Title.ToLower().Contains(searchText.ToLower()))
                .OrderBy(e => e.Id)
                .Skip((page - 1)*pageSize)
                .Take(pageSize)
                .ProjectTo<ExpenseViewModel>()
                .ToList();
            return new ExpenseViewModelList
            {
                PageCount = pageCount,
                TotalItems = totalItems,
                Expenses = expenseList
            };
        }

        public bool UpdateExpense(int id, ExpenseViewModel expenseViewModel)
        {
            var expenseModel = _applicationDbContext.Expenses.Find(id);
            if (expenseModel != null)
            {
                expenseModel.Amount =(double) expenseViewModel.Amount;
                expenseModel.Date = expenseViewModel.Date;
                expenseModel.Title = expenseViewModel.Title;
                expenseModel.Description = expenseViewModel.Description;
                try
                {
                    _applicationDbContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public bool DeleteExpense(int id)
        {
            var expense = _applicationDbContext.Expenses.Find(id);
            if (expense != null)
            {
                try
                {
                    _applicationDbContext.Expenses.Remove(expense);
                    _applicationDbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public bool AddExpense(ExpenseViewModel expenseViewModel)
        {
            try
            {
                var expense = AutoMapper.Mapper.Map<Expense>(expenseViewModel);
                _applicationDbContext.Expenses.Add(expense);
                _applicationDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
    }
}