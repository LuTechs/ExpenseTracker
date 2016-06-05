using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
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
            var expenseViewModelList = _expenseRepository.GetExpensesWithPaginationResult(page, pageSize);

            return Ok(expenseViewModelList);
        }

        [Route("api/expenses/{pageSize}/{page}/{searchText}")]
        public IHttpActionResult GetExpensesWithPaginationBySearchText(int page, int pageSize, string searchText)
        {
            return Ok(_expenseRepository.GetExpensesWithPaginationResultBySearchText(page, pageSize, searchText));
        }

        [Route("api/expenses/{id}")]
        public IHttpActionResult PutExpense(int id, ExpenseViewModel expenseViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_expenseRepository.UpdateExpense(id, expenseViewModel))
            {
                return InternalServerError();
            }
            return
                Ok(new MessageViewModel
                {
                    Message = "the expenseViewModel has been updated with Id:" + expenseViewModel.Id,
                    Status = "Update Ok"
                });
        }

        [Route("api/expenses")]
        public IHttpActionResult PostExpense(ExpenseViewModel expenseViewModel)
        {
            if (!ModelState.IsValid)
            {
              return BadRequest(ModelState);
            }
            if (!_expenseRepository.AddExpense(expenseViewModel))
            {
                return InternalServerError();
            }
            return Ok(new MessageViewModel {Message = "the expense has been added", Status = "Add Ok"});
        }

        // DELETE: api/Expenses/5
        [Route("api/expenses/{id}")]
        public IHttpActionResult DeleteExpense(int id)
        {
            if (!_expenseRepository.DeleteExpense(id))
            {
                return NotFound();
            }
            return Ok(new MessageViewModel {Message = $"the expense id {id} has been delete", Status = "Delete Ok"});
        }
    }
}