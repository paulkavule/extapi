using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ExtApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
           
        }

        [HttpGet("/{nin}")]
        public IActionResult GetDetails(string nin)
        {
            try
            {
                var client = new ThdApi.ThirdPartyInterfaceFacadeNewClient();
                var request = new ThdApi.getPerson { request = new ThdApi.getPersonRequest { nationalId = nin } };
                var result = client.getPersonAsync(request);


                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorCode = 100, ErrorDescription = ex.Message });
            }
            
        }
    }
}
