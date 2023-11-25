using Microsoft.AspNetCore.Mvc;

namespace AuditApp.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "radical :)";
        }
    }
}
