using ConfigLibrary.Models;
using ConfigLibraryApiService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConfigLibraryApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigParamsController : ControllerBase
    {

        private readonly IConfigParamsService _configService;
        public ConfigParamsController(IConfigParamsService configService)
        {
            _configService = configService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _configService.GetAllAsync());
        }
        [HttpGet("{appName}")]
        public async Task<IActionResult> GetByAppName(string appName)
        {
            var config = await _configService.GetByAppNameAsync(appName);
            if (config == null)
            {
                return NotFound();
            }
            return Ok(config);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ConfigParameterModel configData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _configService.CreateAsync(configData);
            return Ok(configData.Id);
        }
        [HttpPut("{appName}")]
        public async Task<IActionResult> Update(string appName, ConfigParameterModel configData)
        {
            var config = await _configService.GetByAppNameAsync(appName);
            if (config == null)
            {
                return NotFound();
            }
            await _configService.UpdateAsync(appName, configData);
            return NoContent();
        }
        [HttpDelete("{appName}")]
        public async Task<IActionResult> Delete(string appName)
        {
            var config = await _configService.GetByAppNameAsync(appName);
            if (config == null)
            {
                return NotFound();
            }
            await _configService.DeleteAsync(config.ApplicationName.ToString());
            return NoContent();
        }
    }
}