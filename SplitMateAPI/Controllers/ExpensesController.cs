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
        public async Task<ActionResult<Expense>> AddExpense(string groupId, [FromBody] Expense expense)
        {
            var result = await _dataService.AddExpenseAsync(groupId, expense);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{expenseId}")]
        public async Task<ActionResult> DeleteExpense(string groupId, string expenseId)
        {
            var result = await _dataService.DeleteExpenseAsync(groupId, expenseId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
