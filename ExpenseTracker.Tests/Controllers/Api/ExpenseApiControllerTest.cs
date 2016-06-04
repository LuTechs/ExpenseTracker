using System.Collections.Generic;
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

        [Fact (DisplayName = "Given page and pageSize then GetExpensesWithPaginationResult should return expected results")]
        public void GetExpenseWithPaginationResult_WithPageAndPageSize_ReturnExpectedResult()
        {
            var mock=new Mock<IExpenseRepository>();
            var expenseApiController=new ExpenseApiController(mock.Object);

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
            Assert.Equal(expenseViewModelList.PageCount,contentResult.Content.PageCount);
            Assert.Equal(expenseViewModelList.TotalItems, contentResult.Content.TotalItems);

        }
    }
}