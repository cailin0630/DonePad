using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DonePadClient.Models;
using DonePadClient.MongoDb;
using GalaSoft.MvvmLight.CommandWpf;

namespace DonePadClient.ViewModel
{
    public class AddTodoViewModel:ViewModelBase
    {
        public AddTodoViewModel()
        {
            ConfirmCommand = new RelayCommand(DoConfirm, () => true);
        }

        private void DoConfirm()
        {
            MongoDbProvide.Insert(new TodoInfos
            {
                Title = Title,
                Text = Text,
                InsertDateTime = DateTime.Now,
                EstimateDateTime = EstimateDateTime,
                UserName = GetInstance<User>().UserName
            });
          
        }
        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        private string _text;

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _estimateDateTime = DateTime.Now;

        public DateTime EstimateDateTime
        {
            get { return _estimateDateTime; }
            set
            {
                _estimateDateTime = value;
                RaisePropertyChanged();
            }
        }

        public ICommand ConfirmCommand { get; set; }
    }
}
