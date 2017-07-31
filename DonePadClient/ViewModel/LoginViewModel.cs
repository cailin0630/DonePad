using DonePadClient.Command;
using DonePadClient.Config;
using DonePadClient.Extensions;
using DonePadClient.Models;
using DonePadClient.View;
using GalaSoft.MvvmLight.CommandWpf;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
            IsAutoLogin = LoginConfigHelper.Config.IsAutoLogin;
            IsKeepPassword = LoginConfigHelper.Config.IsKeepPassword;
            if (LoginConfigHelper.Config.IsAutoLogin)
            {
                //todo auto login
                Task.Factory.StartNew(() =>
                {
                    Name = LoginConfigHelper.Config.UserName;
                    Password = LoginConfigHelper.Config.Password;

                    Thread.Sleep(3000);
                    if (IsAutoLogin)
                    {
                        DoLoginCommand();
                    }
                });
            }
            else if (LoginConfigHelper.Config.IsKeepPassword)
            {
                Name = LoginConfigHelper.Config.UserName;
                Password = LoginConfigHelper.Config.Password;
            }
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
            new RegisterView().ShowDialog();
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

            if (userInfo == null)
            {
                Tips = "用户名或密码错误";
                return;
            }
            Tips = "登录成功";
            UserModel._id = userInfo._id;
            UserModel.UserName = userInfo.UserName;
            UserModel.Image = userInfo.Image;

            LoginConfigHelper.Config.IsAutoLogin = IsAutoLogin;
            LoginConfigHelper.Config.UserName = Name;
            LoginConfigHelper.Config.Password = Password;
            LoginConfigHelper.Config.IsKeepPassword = IsKeepPassword;
            LoginConfigHelper.UpdateConfig();

            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                NotifyToWindow(NotifyCommand.LoginClose);
            }));
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

        private bool _isKeepPassword;

        public bool IsKeepPassword
        {
            get { return _isKeepPassword; }
            set
            {
                _isKeepPassword = value;
                RaisePropertyChanged();
            }
        }

        private bool _isAutoLogin;

        public bool IsAutoLogin
        {
            get { return _isAutoLogin; }
            set
            {
                _isAutoLogin = value;
                RaisePropertyChanged();
            }
        }

        #endregion MyBinding
    }
}