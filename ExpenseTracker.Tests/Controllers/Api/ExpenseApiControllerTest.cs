using ExpenseTracker.Controllers.Api;
using ExpenseTracker.Core.Repositories;
using Moq;
using Xunit;

namespace ExpenseTracker.Tests.Controllers.Api
{
    public class ExpenseApiControllerTest
    {

        [Fact (DisplayName = "Given page and pageSize GetExpensesWithPaginationResult should return expected results")]
        public void GetExpenseWithPaginationResult_WithPageAndPageSize_ReturnExpectedResult()
        {
            var mock=new Mock<IExpenseRepository>();
            var expenseApiController=new ExpenseApiController(mock.Object);


        }
    }
}