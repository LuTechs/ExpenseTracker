using System.Collections.Generic;
using System.Web.Http;
using ExpenseTracker.Core.Repositories;
using ExpenseTracker.ViewModels;

namespace ExpenseTracker.Controllers.Api
{
    public class ExpenseApiController : ApiController
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseApiController(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        [Route("api/expenses/{pageSize}/{page}")]
        public IHttpActionResult GetExpensesWithPaginationResult(int page, int pageSize)
        {
            
            return Ok(new ExpenseViewModelList());
        }
    }
}