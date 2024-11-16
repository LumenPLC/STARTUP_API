using Application.IService;
using Domain.Entities.Common.Params;
using Domain.Entities.Response;
using Domain.Entities.ViewEntities.Menu;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRPM_WEB_API.Controllers
{
    [Route("api/menu")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        public readonly IMenuService _service;
        public MenuController(IMenuService menuService)
        {
            _service = menuService;
        }

        [HttpGet]
        [Route("menu-list")]
        public ActionResult<List<MenuVM>> Get(string USER_ID, int ROLE_ID)
        {
            try
            {
                if (string.IsNullOrEmpty(USER_ID))
                {
                    return BadRequest("Invalid data. Please provide a valid User ID.");
                }

                if (ROLE_ID <= 0)
                {
                    return BadRequest("Invalid data. Please provide a valid Role ID.");
                }

                var result = _service.GetMenu(USER_ID, ROLE_ID);
                if (result == null || result.Count == 0)
                {
                    return NotFound("No data found for the provided User ID and Role ID.");
                }

                ResponseMessage responseMessage = new ResponseMessage();
                responseMessage.data = "Response";
                responseMessage.ResponseObj = result;
                responseMessage.StatusCode = 1;
                responseMessage.Message = "Successfully";
                return Ok(responseMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetOrganizationInfo")]
        public ActionResult<List<OrgInfoGrid>> GetOrganizationInfo([FromQuery] OrgBranchParam orgBranchParam)
        {
            try
            {
                if (orgBranchParam == null)
                {
                    return BadRequest("Invalid data. Please provide valid information.");
                }
                var result = _service.GetOrganizationInfo(orgBranchParam);
                ResponseMessage responseMessage = new ResponseMessage();
                responseMessage.data = "Response";
                responseMessage.ResponseObj = result;
                responseMessage.StatusCode = 1;
                responseMessage.Message = "Succesfully Fetch";
                return Ok(responseMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }


    }
}
