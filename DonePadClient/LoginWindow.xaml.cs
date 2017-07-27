using DonePadClient.Command;
using DonePadClient.View;
using GalaSoft.MvvmLight.Messaging;

namespace DonePadClient
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, nm =>
            {
                if (nm.Sender != DataContext)
                    return;
                if (nm.Notification == NotifyCommand.LoginClose.ToString())
                    Close();
                if (nm.Notification == NotifyCommand.LoginShow.ToString())
                    Show();
            });
        }
    }
}