using System.IO;
using System.Windows;
using DonePadClient.Config;
using DonePadClient.Models;
using DonePadClient.MySql;
using Newtonsoft.Json;


namespace DonePadClient
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //var ret = MySqlWithDapper.InsertOne(new users
            //{
            //    userId = 4,
            //    image = "123213213",
            //    userName = "aaa",
            //    password = "123qwe",
            //    permission = 1
            //});
            var ret = MySqlWithDapper.Update(new users 
            {
                userId = 4,
                userName = "123",
                image = "123",
                password = "123",
                permission = 4
                
            }, "userId",4);
            LoginConfigHelper.ReadConfig();
           MongoDbConfigHelper.ReadConfig();
           base.OnStartup(e);
        }
    }
}