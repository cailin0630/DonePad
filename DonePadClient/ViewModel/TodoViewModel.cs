using DonePadClient.Models;
using DonePadClient.MongoDb;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DonePadClient.ViewModel
{
    public class TodoViewModel : ViewModelBase
    {
        public TodoViewModel()
        {
            FreshCommand = new RelayCommand(DoFreshCommand, () => true);
            DeleteCommand=new RelayCommand(DoDeleteCommand,()=>true);
            DoneCommand=new RelayCommand(DoDoneCommand,()=>true);
        }

        private void DoDoneCommand()
        {
            if (SelectedItem != null)
            {
                var filter = Builders<TodoInfos>.Filter.Eq("_id",SelectedItem._id);
                var update = Builders<TodoInfos>.Update.Set("IsDone", true);
                MongoDbProvide.Update(filter, update);
            }
            TodoList.Remove(SelectedItem);
        }

        private void DoDeleteCommand()
        {
            
            if (SelectedItem != null)
            {
                MongoDbProvide.DeleteOne<TodoInfos>(p=>p._id==SelectedItem._id);
            }
            TodoList.Remove(SelectedItem);
        }


        private void DoFreshCommand()
        {
            try
            {
                var userName = GetInstance<User>().UserName;
                var list = MongoDbProvide.QueryList<TodoInfos>().Where(p => p.UserName == userName);
                TodoList = new ObservableCollection<TodoInfos>(list);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private ObservableCollection<TodoInfos> _todoList;

        public ObservableCollection<TodoInfos> TodoList
        {
            get { return _todoList; }
            set
            {
                _todoList = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _freshCommand;

        public ICommand FreshCommand
        {
            get { return _freshCommand; }
            set
            {
                _freshCommand = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _deleteCommand;

        public ICommand DeleteCommand
        {
            get { return _deleteCommand; }
            set
            {
                _deleteCommand = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _doneCommand;

        public ICommand DoneCommand
        {
            get { return _doneCommand; }
            set
            {
                _doneCommand = value;
                RaisePropertyChanged();
            }
        }

        private TodoInfos _selectedItem;

        public TodoInfos SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }
    }
}