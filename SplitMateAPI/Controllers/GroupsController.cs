using Microsoft.AspNetCore.Mvc;
using SplitMateAPI.Models;
using SplitMateAPI.Services;

namespace SplitMateAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly DataService _dataService;

        public GroupsController(DataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Group>>> GetAllGroups()
        {
            return Ok(await _dataService.GetAllGroupsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(string id)
        {
            var group = await _dataService.GetGroupByIdAsync(id);
            if (group == null)
            {
                return NotFound();
            }
            return Ok(group);
        }

        [HttpPost]
        public async Task<ActionResult<Group>> CreateGroup([FromBody] CreateGroupRequest request)
        {
            var group = await _dataService.CreateGroupAsync(request.GroupName);
            return CreatedAtAction(nameof(GetGroup), new { id = group.GroupId }, group);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGroup(string id)
        {
            var result = await _dataService.DeleteGroupAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("{id}/members")]
        public async Task<ActionResult> AddMember(string id, [FromBody] AddMemberRequest request)
        {
            var result = await _dataService.AddMemberAsync(id, request.MemberName);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}/members/{memberName}")]
        public async Task<ActionResult> RemoveMember(string id, string memberName)
        {
            var result = await _dataService.RemoveMemberAsync(id, memberName);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }

    public class CreateGroupRequest
    {
        public string GroupName { get; set; } = string.Empty;
    }

    public class AddMemberRequest
    {
        public string MemberName { get; set; } = string.Empty;
    }
}
