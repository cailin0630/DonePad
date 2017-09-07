using DonePadClient.Models;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DonePadClient.View;

namespace DonePadClient.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            LogOutCommand = new RelayCommand(DoLogOutCommand, () => true);
            UpdateUserNameAsync();
        }

        private void DoLogOutCommand()
        {
            App.Current.Shutdown();
            Process.Start(Assembly.GetExecutingAssembly().Location);
        }

        private void UpdateUserNameAsync()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(500);
                    UserName = GetInstance<users>().userName;
                    var imagebyte = GetInstance<users>().image;
                    if (imagebyte != null)
                    {
                        App.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            var bmp = new BitmapImage();
                            bmp.BeginInit();
                            bmp.StreamSource = new MemoryStream(imagebyte);
                            bmp.EndInit();
                            UserImage = bmp;
                        }));
                    }
                }
            });
        }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged();
            }
        }

        private ImageSource _userImage;

        public ImageSource UserImage
        {
            get { return _userImage; }
            set
            {
                _userImage = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _logOutCommand;

        public ICommand LogOutCommand
        {
            get { return _logOutCommand; }
            set
            {
                _logOutCommand = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _userInfoCommand;
        public ICommand UserInfoCommand
        {
            get
            {
                return _userInfoCommand!=null?_userInfoCommand:_userInfoCommand=new RelayCommand(() =>
                {
                    new UserInfoView().ShowDialog();
                },()=>true);
            }
        }
    }
}