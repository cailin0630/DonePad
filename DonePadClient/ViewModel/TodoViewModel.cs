using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DonePadClient.Models;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace DonePadClient.ViewModel
{
    public class TodoViewModel:ViewModelBase
    {
        public TodoViewModel()
        {
            Init();
            ConfirmCommand =new RelayCommand(DoConfirm,()=>true);
        }

        private void Init()
        {
            try
            {
                TodoList = MongoDb.MongoDbProvide.QueryList<TodoInfos>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
          
        }
        private void DoConfirm()
        {
            MongoDb.MongoDbProvide.Insert<TodoInfos>(new TodoInfos
            {
                Title = Title,
                Text = Text
            });
            Init();
        }


        private IReadOnlyCollection<TodoInfos> _todoList;
        public IReadOnlyCollection<TodoInfos> TodoList
        {
            get { return _todoList; }
            set
            {
                _todoList = value;
                RaisePropertyChanged();
            }
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

        public ICommand ConfirmCommand { get; set; }
    }
}
