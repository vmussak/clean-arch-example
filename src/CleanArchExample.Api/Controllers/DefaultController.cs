using CleanArchExample.Application;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchExample.Api.Controllers
{
    public class DefaultController : ControllerBase
    {
        protected IActionResult DefaultResponse(ResponseHandler response)
        {
            if(response.Success)
            {
                return Ok(response.Data);
            }

            return StatusCode((int)response.StatusCode, response.Messages);
        }
    }
}
