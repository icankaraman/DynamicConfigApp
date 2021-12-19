using ConfigLibrary.Extensions;
using ConfigLibrary.Models;
using Newtonsoft.Json;
using MongoDB.Driver;

namespace ConfigLibrary.Lib.Services
{
    public class ConfigurationService : HostedService, IConfigurationService
    {
        private readonly string _appName;
        private readonly string _cnnString;
        private readonly IMongoCollection<ConfigParameterModel> mongoCollection;
        private List<ConfigParameterModel> ConfigRecords { get; set; }
        private int _refreshTimerIntervalInMs { get; set; }

        public ConfigurationService(string appName, string cnnString, int refreshTimerIntervalInMs)
        {
            _appName = appName;
            _cnnString = cnnString;
            _refreshTimerIntervalInMs = refreshTimerIntervalInMs;
            var mongoClient = new MongoClient(_cnnString);
            var mongoDb = mongoClient.GetDatabase("ConfigDb");
            mongoCollection = mongoDb.GetCollection<ConfigParameterModel>("ConfigParameterCollection");
            this.StartAsync(new CancellationToken());
        }

        public T GetValue<T>(string key)
        {
            Type type = typeof(T);

            var resultItem = mongoCollection.Find(m => m.IsActive == true && m.ApplicationName.Equals(_appName) && m.Name.Equals(key)).FirstOrDefault();
            if (resultItem == null)
            {
                return default(T);
            }
            else
            {
                return (T)Convert.ChangeType(resultItem.Value, typeof(T));
            }
        }

        private async Task GetConfigRecords()
        {
            try
            {
                ConfigRecords = await mongoCollection.Find(m => m.IsActive == true && m.ApplicationName.Equals(_appName)).ToListAsync();
                WriteToFile();
            }
            catch (Exception ex)
            {
                ReadFromFile();
            }

        }

        private string docPath
        {
            get
            {
                return Path.Combine(Environment.CurrentDirectory, _appName + "-dynamic-config.json");
            }
        }

         private void WriteToFile()
        {
            using (StreamWriter outputFile = new StreamWriter(docPath))
            {
                outputFile.Write(JsonConvert.SerializeObject(ConfigRecords));
            }
        }

        private void ReadFromFile()
        {
            FileStream fileStream = new FileStream(docPath, FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string records = reader.ReadToEnd();
                ConfigRecords = JsonConvert.DeserializeObject<List<ConfigParameterModel>>(records);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken cToken)
        {
            while (!cToken.IsCancellationRequested)
            {
                GetConfigRecords();
                await Task.Delay(_refreshTimerIntervalInMs, cToken);
            }
        }
    }
}
