using DonePadClient.View;
using System.Windows;

namespace DonePadClient
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ViewBase.RegisterView("Login", new LoginWindow());
            //ViewBase.RegisterView("Main", new MainWindow());
        }
    }
}