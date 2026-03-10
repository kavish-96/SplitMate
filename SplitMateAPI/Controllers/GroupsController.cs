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
        public ActionResult<List<Group>> GetAllGroups()
        {
            return Ok(_dataService.GetAllGroups());
        }

        [HttpGet("{id}")]
        public ActionResult<Group> GetGroup(string id)
        {
            var group = _dataService.GetGroupById(id);
            if (group == null)
            {
                return NotFound();
            }
            return Ok(group);
        }

        [HttpPost]
        public ActionResult<Group> CreateGroup([FromBody] CreateGroupRequest request)
        {
            var group = _dataService.CreateGroup(request.GroupName);
            return CreatedAtAction(nameof(GetGroup), new { id = group.GroupId }, group);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteGroup(string id)
        {
            var result = _dataService.DeleteGroup(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("{id}/members")]
        public ActionResult AddMember(string id, [FromBody] AddMemberRequest request)
        {
            var result = _dataService.AddMember(id, request.MemberName);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}/members/{memberName}")]
        public ActionResult RemoveMember(string id, string memberName)
        {
            var result = _dataService.RemoveMember(id, memberName);
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
