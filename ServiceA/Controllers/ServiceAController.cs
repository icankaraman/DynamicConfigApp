using ConfigLibrary.Lib.Services;
using Microsoft.AspNetCore.Mvc;

namespace ServiceA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceAController : ControllerBase
    {
        private readonly ILogger<ServiceAController> _logger;
        public ConfigurationService _configurationService;

        public ServiceAController(ILogger<ServiceAController> logger, ConfigurationService configurationService)
        {
            _logger = logger;
            _configurationService = configurationService;
        }

        [HttpGet(Name = "GetServiceA")]
        public IActionResult Get()
        {
            var siteName = _configurationService.GetValue<string>("SiteName");
            var maxItemCount =  _configurationService.GetValue<int>("MaxCount");
            var isBasketEnabled =  _configurationService.GetValue<bool>("IsBasketEnabled");

            Console.WriteLine($"SiteName:{siteName}");
            Console.WriteLine($"MaxCount:{maxItemCount.ToString()}");
            Console.WriteLine($"IsBasketEnabled:{isBasketEnabled}");
            return Ok();
        }
    }
}