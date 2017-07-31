using System.IO;
using System.Windows;
using DonePadClient.Config;
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
           LoginConfigHelper.ReadConfig();
           MongoDbConfigHelper.ReadConfig();
           base.OnStartup(e);
        }
    }
}