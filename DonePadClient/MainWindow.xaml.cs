using DonePadClient.Command;
using DonePadClient.Models;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using System.Windows;
using System.Windows.Forms;

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
            _currentUser = ServiceLocator.Current.GetInstance<User>(typeof(User).Name).UserName;
            if (_currentUser == null)
                Close();

            InitNotifyIcon();
            StateChanged += MainWindow_StateChanged;
        }

        private readonly string _currentUser;

        private void MainWindow_StateChanged(object sender, System.EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                Hide();
        }

        private void InitNotifyIcon()
        {
            var notifyIcon = new NotifyIcon
            {
                BalloonTipText = $@"hello {_currentUser}",
                Text = $@"DonePad:{_currentUser}",
                Visible = true,
                Icon = new System.Drawing.Icon("0.ico")
            };
            notifyIcon.MouseClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    //todo 控制位置
                }
                else
                {
                    Show();
                    WindowState = WindowState.Normal;
                }
            };

            notifyIcon.ContextMenuStrip = InitContextMenuStrip();

            notifyIcon.ShowBalloonTip(1000);
        }

        private ContextMenuStrip InitContextMenuStrip()
        {
            var contextMenuStrip = new ContextMenuStrip();
            var menuItem1 = new ToolStripMenuItem
            {
                Text = "打开主窗口"
            };
            menuItem1.Click += (s, e) =>
            {
                Show();
                WindowState = WindowState.Normal;
            };
            contextMenuStrip.Items.Add(menuItem1);
            var menuItem2 = new ToolStripMenuItem
            {
                Text = "退出"
            };
            menuItem2.Click += (s, e) =>
            {
                Close();
            };
            contextMenuStrip.Items.Add(menuItem2);
            return contextMenuStrip;
        }
    }
}