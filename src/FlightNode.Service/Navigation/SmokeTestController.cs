using FligthNode.Common.Api.Controllers;
using System.Web.Http;

namespace FlightNode.Service.Navigation
{
    public class SmokeTestController : LoggingController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok("Smoke test success");
        }
    }
}
