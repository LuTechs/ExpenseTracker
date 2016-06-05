using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Http.Results;
using ExpenseTracker.Controllers.Api;
using ExpenseTracker.Core.Repositories;
using ExpenseTracker.ViewModels;
using Moq;
using Xunit;

namespace ExpenseTracker.Tests.Controllers.Api
{
    public class ExpenseApiControllerTest
    {
        [Fact(DisplayName = "Given invalid expense id then DeleteExpense with  should return not found error")]
        public void DeleteExpenseWithInvalidId()
        {
            var mock = new Mock<IExpenseRepository>();
            var exepnseApiController = new ExpenseApiController(mock.Object);
            mock.Setup(x => x.DeleteExpense(1)).Returns(false);
            var result = exepnseApiController.DeleteExpense(1);
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact(DisplayName = "Given valid expense id then DeleteExpense with  should  delete the record")]
        public void DeleteExpenseWithValidId()
        {
            var mock = new Mock<IExpenseRepository>();
            var exepnseApiController = new ExpenseApiController(mock.Object);
            mock.Setup(x => x.DeleteExpense(1)).Returns(true);
            var result = exepnseApiController.DeleteExpense(1);
            Assert.NotNull(result);
            Assert.IsType<OkNegotiatedContentResult<MessageViewModel>>(result);
        }

        [Fact(
            DisplayName =
                "Given page, pageSize and searchText then GetExpensesWithPaginationBySearchText should return expected result"
            )]
        public void GetExpensesWithPaginationBySearchTextShouldReturnExpectedResult()
        {
            var mock = new Mock<IExpenseRepository>();
            var exepnseApiController = new ExpenseApiController(mock.Object);

            var expenseViewModelList = new ExpenseViewModelList
            {
                PageCount = 10,
                TotalItems = 100,
                Expenses = new List<ExpenseViewModel>()
            };
            mock.Setup(x => x.GetExpensesWithPaginationResultBySearchText(1, 10, "Abc")).Returns(expenseViewModelList);

            var result = exepnseApiController.GetExpensesWithPaginationBySearchText(1, 10, "Abc");
            var contentResult = result as OkNegotiatedContentResult<ExpenseViewModelList>;

            Assert.NotNull(result);
            Assert.IsType<OkNegotiatedContentResult<ExpenseViewModelList>>(result);
            Assert.Equal(expenseViewModelList.PageCount, contentResult.Content.PageCount);
            Assert.Equal(expenseViewModelList.TotalItems, contentResult.Content.TotalItems);
        }

        [Fact(
            DisplayName = "Given page and pageSize then GetExpensesWithPaginationResult should return expected results")
        ]
        public void GetExpenseWithPaginationResult_WithPageAndPageSize_ReturnExpectedResult()
        {
            var mock = new Mock<IExpenseRepository>();
            var expenseApiController = new ExpenseApiController(mock.Object);

            var expenseViewModelList = new ExpenseViewModelList
            {
                PageCount = 10,
                TotalItems = 100,
                Expenses = new List<ExpenseViewModel>()
            };
            mock.Setup(x => x.GetExpensesWithPaginationResult(1, 10)).Returns(expenseViewModelList);

            var result = expenseApiController.GetExpensesWithPaginationResult(1, 10);
            var contentResult = result as OkNegotiatedContentResult<ExpenseViewModelList>;
            Assert.NotNull(result);
            Assert.IsType<OkNegotiatedContentResult<ExpenseViewModelList>>(result);
            Assert.Equal(expenseViewModelList.PageCount, contentResult.Content.PageCount);
            Assert.Equal(expenseViewModelList.TotalItems, contentResult.Content.TotalItems);
        }

        [Fact(DisplayName = "Given invalid model then PostExpense with  should return error")]
        public void PostExpenseWithInValidModelShouldUpdateTheRecord()
        {
            var mock = new Mock<IExpenseRepository>();
            var exepnseApiController = new ExpenseApiController(mock.Object);
            var expenseViewModel = new ExpenseViewModel();

            mock.Setup(x => x.AddExpense(expenseViewModel)).Returns(true);

            var validationContext = new ValidationContext(expenseViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(expenseViewModel, validationContext, validationResults);

            foreach (var validationResult in validationResults)
            {
                exepnseApiController.ModelState.AddModelError(validationResult.MemberNames.First(),
                    validationResult.ErrorMessage);
            }
            var result = exepnseApiController.PostExpense(expenseViewModel);
            Assert.NotNull(result);
            Assert.IsType<InvalidModelStateResult>(result);
        }


        [Fact(DisplayName = "Given valid model then PostExpense with  should  add new record")]
        public void PostExpenseWithValidModelShouldUpdateTheRecord()
        {
            var mock = new Mock<IExpenseRepository>();
            var exepnseApiController = new ExpenseApiController(mock.Object);
            var expenseViewModel = new ExpenseViewModel {Amount = 20, Title = "Buy Sth", Date = DateTime.UtcNow};

            mock.Setup(x => x.AddExpense(expenseViewModel)).Returns(true);

            var validationContext = new ValidationContext(expenseViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(expenseViewModel, validationContext, validationResults);

            foreach (var validationResult in validationResults)
            {
                exepnseApiController.ModelState.AddModelError(validationResult.MemberNames.First(),
                    validationResult.ErrorMessage);
            }
            var result = exepnseApiController.PostExpense(expenseViewModel);
            Assert.NotNull(result);
            Assert.IsType<OkNegotiatedContentResult<MessageViewModel>>(result);
        }

        [Fact(DisplayName = "Given invalid model then PutExpense should  return error")]
        public void PutExpenseWithInvalidModelShouldUpdateTheRecord()
        {
            var mock = new Mock<IExpenseRepository>();
            var exepnseApiController = new ExpenseApiController(mock.Object);
            var expenseViewModel = new ExpenseViewModel();

            mock.Setup(x => x.UpdateExpense(1, expenseViewModel)).Returns(true);

            var validationContext = new ValidationContext(expenseViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(expenseViewModel, validationContext, validationResults);

            foreach (var validationResult in validationResults)
            {
                exepnseApiController.ModelState.AddModelError(validationResult.MemberNames.First(),
                    validationResult.ErrorMessage);
            }
            var result = exepnseApiController.PutExpense(1, expenseViewModel);
            Assert.NotNull(result);
            Assert.IsType<InvalidModelStateResult>(result);
        }

        [Fact(DisplayName = "Given valid model then PutExpense with  should  update the record")]
        public void PutExpenseWithValidModelShouldUpdateTheRecord()
        {
            var mock = new Mock<IExpenseRepository>();
            var exepnseApiController = new ExpenseApiController(mock.Object);
            var expenseViewModel = new ExpenseViewModel {Amount = 20, Title = "Buy Sth", Date = DateTime.UtcNow};

            mock.Setup(x => x.UpdateExpense(1, expenseViewModel)).Returns(true);

            var validationContext = new ValidationContext(expenseViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(expenseViewModel, validationContext, validationResults);

            foreach (var validationResult in validationResults)
            {
                exepnseApiController.ModelState.AddModelError(validationResult.MemberNames.First(),
                    validationResult.ErrorMessage);
            }
            var result = exepnseApiController.PutExpense(1, expenseViewModel);
            Assert.NotNull(result);
            Assert.IsType<OkNegotiatedContentResult<MessageViewModel>>(result);
        }
    }
}