using Microsoft.AspNetCore.Mvc;
using SplitMateAPI.Models;
using SplitMateAPI.Services;

namespace SplitMateAPI.Controllers
{
    [ApiController]
    [Route("api/groups/{groupId}/[controller]")]
    public class BalancesController : ControllerBase
    {
        private readonly DataService _dataService;
        private readonly BalanceService _balanceService;

        public BalancesController(DataService dataService, BalanceService balanceService)
        {
            _dataService = dataService;
            _balanceService = balanceService;
        }

        [HttpGet]
        public ActionResult<List<Balance>> GetBalances(string groupId)
        {
            var group = _dataService.GetGroupById(groupId);
            if (group == null)
            {
                return NotFound();
            }

            var balances = _balanceService.CalculateBalances(group);
            return Ok(balances);
        }
    }
}
