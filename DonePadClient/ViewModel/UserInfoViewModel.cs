using DonePadClient.Extensions;
using DonePadClient.Models;
using GalaSoft.MvvmLight.CommandWpf;
using MongoDB.Driver;
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace DonePadClient.ViewModel
{
    public class UserInfoViewModel : ViewModelBase
    {
        public UserInfoViewModel()
        {
            GetImageCommand = new RelayCommand(DoGetImage, () => true);
            Name = GetInstance<User>().UserName;
            var imagebyte = GetInstance<User>().Image;
            ImageSource = imagebyte.Byte2Image();
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

        private ICommand _getImageCommand;

        public ICommand GetImageCommand
        {
            get { return _getImageCommand; }
            set
            {
                _getImageCommand = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _updateCommand;

        public ICommand UpdateCommand
        {
            get
            {
                return _updateCommand ?? (_updateCommand = new RelayCommand(() =>
                {
                var filter = Builders<User>.Filter.Eq("_id", GetInstance<User>()._id);
                var update = Builders<User>.Update.Set("Image", File.ReadAllBytes(_imageName));
                var ret = MongoDb.MongoDbProvide.Update(filter, update);
                Tips = ret ? "头像修改成功" : "头像修改失败";
                }, () => true));
            }
        }

        private string _imageName;

        private void DoGetImage()
        {
            var openfile = new OpenFileDialog
            {
                Filter = "jpg|*.jpg*|bmp|*.bmp*|png|*.png*|所有文件|*.*"
            };
            var showDialog = openfile.ShowDialog();
            if (showDialog != null && !showDialog.Value)
            {
                return;
            }
            _imageName = openfile.FileName;
            ImageSource = new BitmapImage(new Uri(_imageName));
        }
    }
}