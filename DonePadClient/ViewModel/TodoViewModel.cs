using DonePadClient.Models;
using DonePadClient.MongoDb;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DonePadClient.ViewModel
{
    public class TodoViewModel : ViewModelBase
    {
        public TodoViewModel()
        {
            Init();
            ConfirmCommand = new RelayCommand(DoConfirm, () => true);
        }

        private void Init()
        {
            try
            {
                TodoList = MongoDbProvide.QueryList<TodoInfos>().Where(p => p.UserName == GetInstance<User>().UserName).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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