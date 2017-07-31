using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DonePadClient.Config
{
    public class MongoDbConfigHelper
    {
        private const string ConfigUrl = "Config/MongoDbConfig.json";
        public static void ReadConfig()
        {
            Config = JsonConvert.DeserializeObject<MongoDbConfig>(File.ReadAllText(ConfigUrl,Encoding.UTF8));
        }

        public static void UpdateConfig()
        {
            File.WriteAllText(ConfigUrl, JsonConvert.SerializeObject(Config), Encoding.UTF8);
        }

        public static MongoDbConfig Config;

        public class MongoDbConfig
        {
            public string ConnectionString { get; set; }
            public string DataBaseName { get; set; }
        }
       
    }
}