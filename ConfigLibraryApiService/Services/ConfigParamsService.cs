using ConfigLibrary.Models;
using ConfigLibraryApiService.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ConfigLibraryApiService.Services
{
    public class ConfigParamsService : IConfigParamsService
    {
        private readonly IMongoCollection<ConfigParameterModel> _collection;
        private readonly ParamsConfiguration _settings;
        public ConfigParamsService(IOptions<ParamsConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _collection = database.GetCollection<ConfigParameterModel>(_settings.CollectionName);

        }
        public async Task<ConfigParameterModel> CreateAsync(ConfigParameterModel config)
        {
            await _collection.InsertOneAsync(config);
            return config;
        }

        public async Task DeleteAsync(string appName, string appVariable)
        {
            var collec = await _collection.Find(c => c.ApplicationName.Equals(appName) && c.Name.Equals(appVariable) && c.IsActive == true).FirstOrDefaultAsync();
            if (collec != null)
            {
               collec.IsActive = false;
            }
            await UpdateAsync(appName,appVariable,collec);
        }

        public async Task<List<ConfigParameterModel>> GetAllAsync()
        {
            return await _collection.Find(c => c.IsActive == true).ToListAsync();
        }

        public async Task<ConfigParameterModel> GetByAppNameAsync(string appName, string appVariable)
        {
            return await _collection.Find<ConfigParameterModel>(c => c.ApplicationName == appName && c.Name.Equals(appVariable) && c.IsActive == true).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string appName, string appVariable, ConfigParameterModel config)
        {
            await _collection.ReplaceOneAsync(c => c.ApplicationName.Equals(appName) && c.Name.Equals(appVariable) && c.IsActive == true, config);
        }
    }
}
