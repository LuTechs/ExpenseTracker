using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Http.Results;
using ExpenseTracker.Controllers.Api;
using ExpenseTracker.Core.Helpers;
using ExpenseTracker.Core.Repositories;
using ExpenseTracker.IntergrationTest.TestHelpers;
using ExpenseTracker.ViewModels;
using Xunit;

namespace ExpenseTrackerIntegrationTest.Controllers.Api
{
    public class ExpenseApiControllerIntergrationTest : IClassFixture<DatabaseFixture>
    {
        public ExpenseApiControllerIntergrationTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _exepnseApiController = new ExpenseApiController(new ExpenseRepository(_fixture.DbContext));
        }

        private readonly DatabaseFixture _fixture;
        private readonly ExpenseApiController _exepnseApiController;

        [Fact(DisplayName = "Given invalid expense id then DeleteExpense with  should return not found error")]
        public void DeleteExpenseWithInvalidId()
        {
            var result = _exepnseApiController.DeleteExpense(999);
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact(DisplayName = "Given valid expense id then DeleteExpense with  should  delete the record")]
        public void DeleteExpenseWithValidId()
        {
            var result = _exepnseApiController.DeleteExpense(1);
            Assert.NotNull(result);
            Assert.IsType<OkNegotiatedContentResult<MessageViewModel>>(result);
        }


        [Fact(
            DisplayName =
                "Given page, pageSize and searchText then GetExpensesWithPaginationBySearchText should return expected result"
            )]
        public void GetExpensesWithPaginationBySearchTextShouldReturnExpectedResult()
        {
            var result = _exepnseApiController.GetExpensesWithPaginationBySearchText(1, 10, "Nothing");
            var contentResult = result as OkNegotiatedContentResult<ExpenseViewModelList>;

            Assert.NotNull(result);
            Assert.IsType<OkNegotiatedContentResult<ExpenseViewModelList>>(result);
            Assert.Equal(0, contentResult.Content.PageCount);
            Assert.Equal(0, contentResult.Content.TotalItems);
        }


        [Fact(
            DisplayName = "Given page and pageSize then GetExpensesWithPaginationResult should return expected results")
        ]
        public void GetExpenseWithPaginationResult_WithPageAndPageSize_ReturnExpectedResult()
        {
            var totalRecord = _fixture.DbContext.Expenses.Count();
            var pageCount = PageCount.Count(totalRecord, 10);

            var result = _exepnseApiController.GetExpensesWithPaginationResult(1, 10);
            var contentResult = result as OkNegotiatedContentResult<ExpenseViewModelList>;


            Assert.NotNull(result);
            Assert.IsType<OkNegotiatedContentResult<ExpenseViewModelList>>(result);
            Assert.Equal(pageCount, contentResult.Content.PageCount);
            Assert.Equal(totalRecord, contentResult.Content.TotalItems);
        }


        [Fact(DisplayName = "Given invalid model then PostExpense with  should return error")]
        public void PostExpenseWithInValidModelShouldUpdateTheRecord()
        {
            var expenseViewModel = new ExpenseViewModel();

            var validationContext = new ValidationContext(expenseViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(expenseViewModel, validationContext, validationResults);

            foreach (var validationResult in validationResults)
            {
                _exepnseApiController.ModelState.AddModelError(validationResult.MemberNames.First(),
                    validationResult.ErrorMessage);
            }
            var result = _exepnseApiController.PostExpense(expenseViewModel);
            Assert.NotNull(result);
            Assert.IsType<InvalidModelStateResult>(result);
        }

        [Fact(DisplayName = "Given valid model then PostExpense with  should  add new record")]
        public void PostExpenseWithValidModelShouldUpdateTheRecord()
        {
            var expenseViewModel = new ExpenseViewModel {Amount = 20, Title = "Buy Sth", Date = DateTime.UtcNow};


            var validationContext = new ValidationContext(expenseViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(expenseViewModel, validationContext, validationResults);

            foreach (var validationResult in validationResults)
            {
                _exepnseApiController.ModelState.AddModelError(validationResult.MemberNames.First(),
                    validationResult.ErrorMessage);
            }
            var result = _exepnseApiController.PostExpense(expenseViewModel);
            Assert.NotNull(result);
            Assert.IsType<OkNegotiatedContentResult<MessageViewModel>>(result);
        }

        [Fact(DisplayName = "Given invalid model then PutExpense should  return error")]
        public void PutExpenseWithInvalidModelShouldUpdateTheRecord()
        {
            var expenseViewModel = new ExpenseViewModel();
            var validationContext = new ValidationContext(expenseViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(expenseViewModel, validationContext, validationResults);

            foreach (var validationResult in validationResults)
            {
                _exepnseApiController.ModelState.AddModelError(validationResult.MemberNames.First(),
                    validationResult.ErrorMessage);
            }
            var result = _exepnseApiController.PutExpense(1, expenseViewModel);
            Assert.NotNull(result);
            Assert.IsType<InvalidModelStateResult>(result);
        }

        [Fact(DisplayName = "Given valid model then PutExpense with  should  update the record")]
        public void PutExpenseWithValidModelShouldUpdateTheRecord()
        {
            var expenseViewModel = new ExpenseViewModel
            {
                Amount = 20,
                Title = "Buy iPad Pro 12 inch",
                Date = DateTime.UtcNow
            };


            var validationContext = new ValidationContext(expenseViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(expenseViewModel, validationContext, validationResults);

            foreach (var validationResult in validationResults)
            {
                _exepnseApiController.ModelState.AddModelError(validationResult.MemberNames.First(),
                    validationResult.ErrorMessage);
            }
            var result = _exepnseApiController.PutExpense(2, expenseViewModel);
            Assert.NotNull(result);
            Assert.IsType<OkNegotiatedContentResult<MessageViewModel>>(result);
        }
    }
}