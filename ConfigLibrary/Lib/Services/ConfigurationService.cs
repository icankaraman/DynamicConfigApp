using ConfigLibrary.Extensions;
using ConfigLibrary.Models;
using MongoDB.Driver;

namespace ConfigLibrary.Lib.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly string _appName;
        private readonly string _cnnString;
        private readonly IMongoCollection<ConfigParameterModel> mongoCollection;

        public ConfigurationService(string appName, string cnnString)
        {
            _appName = appName;
            _cnnString = cnnString;
            var mongoClient = new MongoClient(_cnnString);
            //var databasenames = mongoClient.ListDatabases();
            //var mongoDb = mongoClient.GetDatabase("ConfigDb");

            //mongoCollection = mongoDb.GetCollection<ConfigParameterModel>("ConfigParameterCollection");
            //mongoCollection.InsertOne(new ConfigParameterModel
            //{
            //    ApplicationName = "App1",
            //    CreateDate = DateTime.Now,
            //    UpdateDate = DateTime.Now,
            //    IsActive = true,
            //    Name = "Try",
            //    Type = (long)Models.Enums.TypeEnum.Bool,
            //    Value = "1"
            //});
        }

        public async Task<T> GetValue<T>(string key)
        {
            Type type = typeof(T);

            var resultList = await mongoCollection.Find(m => m.IsActive == true && m.ApplicationName.Equals(_appName) && m.Name.Equals(key)).ToListAsync();
            foreach (var item in resultList)
            {
                if ((item.Type).GetDescription().ToLower().Equals(type.Name.ToLower()))
                    return (T)Convert.ChangeType(item.Value, typeof(T));
            }
            return (T)Convert.ChangeType(null, typeof(T));
        }
    }
}
