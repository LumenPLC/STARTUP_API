using Application.IService;
using Domain.Entities.Response;
using Domain.Entities.ViewEntities.Role;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace NEW_API.Controllers
{
    [Route("api/Roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {


        public readonly IRoleService _service;
        public RoleController(IRoleService roleService)
        {
            _service = roleService;
        }





        [HttpGet]
        [Route("GetRoleList")]
        public IActionResult GetRoleList()
        {

            List<RoleModel> roleList = _service.GetRoleList();
            ResponseMessage responseMessage = new ResponseMessage();
            responseMessage.StatusCode = 1;
            responseMessage.Message = "Role List";
            responseMessage.ResponseObj = roleList;
            return Ok(responseMessage);
        }













    }
}
