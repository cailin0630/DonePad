using System;
using System.IO;
using DonePadClient.Command;
using DonePadClient.Extensions;
using DonePadClient.Models;
using GalaSoft.MvvmLight.CommandWpf;
using MongoDB.Driver;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;

namespace DonePadClient.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(DoLoginCommand, () => true);
            RegisterCommand = new RelayCommand(DoRegisterCommand, () => true);
            UpdateCommand = new RelayCommand(DoUpdateCommand, () => true);
            UserModel = GetInstance<User>();
            GetImage=new RelayCommand(DoGetImage,()=>true);
        }

        private void DoGetImage()
        {
           var openfile=new OpenFileDialog();
            openfile.ShowDialog();
            _imageName = openfile.FileName;
            ImageSource = new BitmapImage(new Uri(_imageName));

        }

        private string _imageName;
        public User UserModel { get; set; }

        private void DoUpdateCommand()
        {
            if (Name.IsNullOrWhiteSpace() || Password.IsNullOrWhiteSpace())
            {
                Tips = "用户名或者密码为空";
                return;
            }
            var userInfo = MongoDb.MongoDbProvide.QueryList<User>().FirstOrDefault(p => p.UserName == Name);

            if (userInfo == null)
            {
                Tips = "用户名未注册";
                return;
            }
            var filter = Builders<User>.Filter.Eq("_id", userInfo._id);
            var update = Builders<User>.Update.Set("Password", Password.ToMd5EncryptString());
            var ret = MongoDb.MongoDbProvide.Update(filter, update);
            Tips = ret ? "密码修改成功" : "密码修改失败";
        }

        private void DoRegisterCommand()
        {
            if (Name.IsNullOrWhiteSpace() || Password.IsNullOrWhiteSpace())
            {
                Tips = "用户名或者密码为空";
                return;
            }
            var ret = MongoDb.MongoDbProvide.QueryList<User>().FirstOrDefault(p => p.UserName == Name) != null;
            if (ret)
            {
                Tips = "已注册";
                return;
            }
            MongoDb.MongoDbProvide.Insert(new User
            {
                UserName = Name,
                Password = Password.ToMd5EncryptString(),
                Image = File.ReadAllBytes(_imageName)
            });
            Tips = "注册成功";
        }

        private void DoLoginCommand()
        {
            if (Name.IsNullOrWhiteSpace() || Password.IsNullOrWhiteSpace())
            {
                Tips = "用户名或者密码为空";
                return;
            }
            var userInfo = MongoDb.MongoDbProvide.QueryList<User>()
                .FirstOrDefault(p => p.UserName == Name && p.Password == Password.ToMd5EncryptString());
           
            if (userInfo==null)
            {
                Tips = "用户名或密码错误";
                return;
            }
            Tips = "登录成功";
            UserModel.UserName = userInfo.UserName;
            UserModel.Password = userInfo.Password;
            UserModel.Image = userInfo.Image;
            NotifyToWindow(NotifyCommand.LoginClose);
        }

        #region MyBinding

        private string _tips;

        public string Tips
        {
            get { return _tips; }
            set
            {
                _tips = value;
                RaisePropertyChanged();
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _loginCommand;

        public ICommand LoginCommand
        {
            get { return _loginCommand; }
            set
            {
                _loginCommand = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _registerCommand;

        public ICommand RegisterCommand
        {
            get { return _registerCommand; }
            set
            {
                _registerCommand = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _updateCommand;

        public ICommand UpdateCommand
        {
            get { return _updateCommand; }
            set
            {
                _updateCommand = value;
                RaisePropertyChanged();
            }
        }

        private ImageSource _imageSource;

        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _getImage;

        public ICommand GetImage
        {
            get { return _getImage; }
            set
            {
                _getImage = value;
                RaisePropertyChanged();
            }
        }

        #endregion MyBinding
    }
}