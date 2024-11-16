using Application.IService;
using Domain.Entities.Response;
using Domain.Entities.ViewEntities.Auth;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NEW_WEB_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthService _service;

        public AuthController(IAuthService authService)
        {
            _service = authService;

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login([FromBody] UserParams user)
        {

            AdminUserMstVM loginData = _service.GetLoggedData(user);
            ResponseMessage responseMessage = new ResponseMessage();
            if (loginData.STATUS != null)
            {

                responseMessage.StatusCode = 1;
                responseMessage.Message = "Login Successfully";
                responseMessage.ResponseObj = loginData;
            }
            else
            {
                responseMessage.StatusCode = 0;
                responseMessage.Message = "Invalid User or Password";
                responseMessage.ResponseObj = "";
            }
            // return Ok("");
            return Ok(responseMessage);
        }



    }
}
