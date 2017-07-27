using DonePadClient.Command;
using DonePadClient.Models;
using DonePadClient.View;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;

namespace DonePadClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, nm =>
            {
                if (nm.Sender != DataContext)
                    return;
                if (nm.Notification == NotifyCommand.MainClose.ToString())
                    Close();
                if (nm.Notification == NotifyCommand.MainShow.ToString())
                    Show();
            });
            new LoginWindow().ShowDialog();
            if (ServiceLocator.Current.GetInstance<User>(typeof(User).Name).UserName == null)
                Close();
        }
    }
}