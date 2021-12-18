using ConfigLibrary.Models;

namespace ConfigLibraryApiService.Services
{
    public interface IConfigParamsService
    {
        Task<List<ConfigParameterModel>> GetAllAsync();
        Task<ConfigParameterModel> GetByAppNameAsync(string appName);
        Task<ConfigParameterModel> CreateAsync(ConfigParameterModel config);
        Task UpdateAsync(string appName, ConfigParameterModel config);
        Task DeleteAsync(string appName);
    }
}
