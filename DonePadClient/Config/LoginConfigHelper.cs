using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DonePadClient.Config
{
    public class LoginConfigHelper
    {
        private const string ConfigUrl = "Config/LoginConfig.json";
        public static void ReadConfig()
        {
            if (!File.Exists(ConfigUrl))
            {
                Config=new LoginConfig
                {
                    IsKeepPassword=false,
                    IsAutoLogin = false,
                    UserName = null,
                    Password = null
                };
                UpdateConfig();
            }
            else
            {
                var jsonStr = File.ReadAllText(ConfigUrl, Encoding.UTF8);
                Config = JsonConvert.DeserializeObject<LoginConfig>(jsonStr);
            }
           
        }

        public static void UpdateConfig()
        {
            File.WriteAllText(ConfigUrl, JsonConvert.SerializeObject(Config), Encoding.UTF8);
        }

        public static LoginConfig Config;

       
        
      
    }
    public class LoginConfig
    {

        public bool IsKeepPassword { get; set; }

        public bool IsAutoLogin { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}