using ExpenseTracker.Controllers.Api;
using ExpenseTracker.Core.Repositories;
using Moq;
using Xunit;

namespace ExpenseTracker.Tests.Controllers.Api
{
    public class ExpenseApiControllerTest
    {

        [Fact (DisplayName = "Requst GetExpenses")]
        public void GetExpenseWithPaginationResult_WithPageAndPageSize_ReturnExpectedResult()
        {
            var mock=new Mock<IExpenseRepository>();
            var expenseApiController=new ExpenseApiController(mock.Object);


        }
    }
}