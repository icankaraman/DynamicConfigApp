using ConfigLibrary.Lib.Services;
using Microsoft.AspNetCore.Mvc;

namespace ServiceB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceBController : ControllerBase
    {
        private readonly ILogger<ServiceBController> _logger;
        public ConfigurationService _configurationService;

        public ServiceBController(ILogger<ServiceBController> logger, ConfigurationService configurationService)
        {
            _logger = logger;
            _configurationService = configurationService;
        }

        [HttpGet(Name = "GetServiceB")]
        public IActionResult Get()
        {
            var siteName = _configurationService.GetValue<string>("SiteName");
            var maxItemCount = _configurationService.GetValue<int>("MaxCount");
            var isBasketEnabled = _configurationService.GetValue<bool>("IsBasketEnabled");

            Console.WriteLine($"SiteName:{siteName}");
            Console.WriteLine($"MaxCount:{maxItemCount.ToString()}");
            Console.WriteLine($"IsBasketEnabled:{isBasketEnabled}");
            return Ok();
        }
    }
}