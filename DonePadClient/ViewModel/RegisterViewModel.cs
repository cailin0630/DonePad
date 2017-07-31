using DonePadClient.Extensions;
using DonePadClient.Models;
using GalaSoft.MvvmLight.CommandWpf;
using System.Linq;
using System.Windows.Input;

namespace DonePadClient.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(DoRegisterCommand, () => true);
        }
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

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
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

        private void DoRegisterCommand()
        {
            if (Name.IsNullOrWhiteSpace() || Password.IsNullOrWhiteSpace())
            {
                Tips = "userName or password is null";
                return;
            }
            if (ConfirmPassword.IsNullOrWhiteSpace())
            {
                Tips = "confirmpassword is null";
                return;
            }
            if (Password != ConfirmPassword)
            {
                Tips = "password and confirmpassword isnot equal";
                return;
            }
            var ret = MongoDb.MongoDbProvide.QueryList<User>().FirstOrDefault(p => p.UserName == Name) != null;
            if (ret)
            {
                Tips = "the username has been registered";
                return;
            }
            MongoDb.MongoDbProvide.Insert(new User
            {
                UserName = Name,
                Password = Password.ToMd5EncryptString(),
            });
            Tips = "register success";
        }
    }
}