using Application.IService;

using Domain.Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NEW_WEB_API.Controllers
{
    [Authorize]
    [Route("api/setup")]
    [ApiController]
    public class SetupController : ControllerBase
    {
        public readonly ISetupService _service;

        public SetupController(ISetupService setupService)
        {
            _service = setupService;
            
        }

       

    }
}
