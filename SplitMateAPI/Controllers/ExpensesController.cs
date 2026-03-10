using Microsoft.AspNetCore.Mvc;
using SplitMateAPI.Models;
using SplitMateAPI.Services;

namespace SplitMateAPI.Controllers
{
    [ApiController]
    [Route("api/groups/{groupId}/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly DataService _dataService;

        public ExpensesController(DataService dataService)
        {
            _dataService = dataService;
        }

        [HttpPost]
        public ActionResult<Expense> AddExpense(string groupId, [FromBody] Expense expense)
        {
            var result = _dataService.AddExpense(groupId, expense);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{expenseId}")]
        public ActionResult DeleteExpense(string groupId, string expenseId)
        {
            var result = _dataService.DeleteExpense(groupId, expenseId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
