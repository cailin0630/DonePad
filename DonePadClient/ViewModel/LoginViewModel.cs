﻿using DonePadClient.Extensions;
using DonePadClient.Models;
using DonePadClient.View;
using GalaSoft.MvvmLight.CommandWpf;
using System.Linq;
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
        }

        public User UserModel { get; set; }
        private void DoUpdateCommand()
        {
            if (Name.IsNullOrWhiteSpace() || Password.IsNullOrWhiteSpace())
            {
                Tips = "用户名或者密码为空";
                return;
            }
            var ret = MongoDb.MongoDbProvide.QueryList<User>().FirstOrDefault(p => p.UserName == Name) != null;
            if (!ret)
            {
                Tips = "用户名未注册";
                return;
            }
            //MongoDb.MongoDbProvide.Update();
        }

        private void DoRegisterCommand()
        {
            if (Name.IsNullOrWhiteSpace() || Password.IsNullOrWhiteSpace())
            {
                Tips = "用户名或者密码为空";
                return;
            }
            var ret = MongoDb.MongoDbProvide.QueryList<User>().FirstOrDefault(p => p.UserName == Name && p.Password == Password) != null;
            if (ret)
            {
                Tips = "已注册";
                return;
            }
            MongoDb.MongoDbProvide.Insert(new User
            {
                UserName = Name,
                Password = Password
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
            var ret = MongoDb.MongoDbProvide.QueryList<User>().FirstOrDefault(p => p.UserName == Name && p.Password == Password) != null;
            if (!ret)
            {
                Tips = "用户名或密码错误";
                return;
            }
            Tips = "登录成功";
            UserModel.UserName = Name;
            UserModel.Password = Password;
            ViewBase.CloseView("Login");
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
        #endregion
    }
}