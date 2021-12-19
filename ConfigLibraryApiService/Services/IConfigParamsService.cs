using ConfigLibrary.Models;

namespace ConfigLibraryApiService.Services
{
    public interface IConfigParamsService
    {
        Task<List<ConfigParameterModel>> GetAllAsync();
        Task<ConfigParameterModel> GetByAppNameAsync(string appName, string appVariable);
        Task<ConfigParameterModel> CreateAsync(ConfigParameterModel config);
        Task UpdateAsync(string appName, string appVariable, ConfigParameterModel config);
        Task DeleteAsync(string appName, string appVariable);
    }
}
