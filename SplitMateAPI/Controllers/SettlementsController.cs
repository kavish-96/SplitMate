using Microsoft.AspNetCore.Mvc;
using SplitMateAPI.Models;
using SplitMateAPI.Services;

namespace SplitMateAPI.Controllers
{
    [ApiController]
    [Route("api/groups/{groupId}/[controller]")]
    public class SettlementsController : ControllerBase
    {
        private readonly DataService _dataService;
        private readonly SettlementService _settlementService;

        public SettlementsController(DataService dataService, SettlementService settlementService)
        {
            _dataService = dataService;
            _settlementService = settlementService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Transaction>>> GetSettlements(string groupId)
        {
            var group = await _dataService.GetGroupByIdAsync(groupId);
            if (group == null)
            {
                return NotFound();
            }

            var settlements = _settlementService.CalculateOptimalSettlements(group);
            return Ok(settlements);
        }

        [HttpPost]
        public async Task<ActionResult> SettleUp(string groupId, [FromBody] Settlement settlement)
        {
            var result = await _dataService.AddSettlementAsync(groupId, settlement);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
