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
        public ActionResult<List<Transaction>> GetSettlements(string groupId)
        {
            var group = _dataService.GetGroupById(groupId);
            if (group == null)
            {
                return NotFound();
            }

            var settlements = _settlementService.CalculateOptimalSettlements(group);
            return Ok(settlements);
        }

        [HttpPost]
        public ActionResult SettleUp(string groupId, [FromBody] Settlement settlement)
        {
            var result = _dataService.AddSettlement(groupId, settlement);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
