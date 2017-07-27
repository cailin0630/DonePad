using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DonePadClient.Models;
using DonePadClient.MongoDb;
using GalaSoft.MvvmLight.CommandWpf;

namespace DonePadClient.ViewModel
{
    public class DoneViewModel:ViewModelBase
    {
        public DoneViewModel()
        {
            FreshCommand = new RelayCommand(DoFreshCommand, () => true);
            DeleteCommand = new RelayCommand(DoDeleteCommand, () => true);
        }
        private void DoFreshCommand()
        {
            try
            {
                var userName = GetInstance<User>().UserName;
                var list = MongoDbProvide.QueryList<TodoInfos>().Where(p => p.UserName == userName && p.IsDone);
                DoneList = new ObservableCollection<TodoInfos>(list);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DoDeleteCommand()
        {
            if (SelectedItem == null) return;
            MongoDbProvide.Delete<TodoInfos>(p => p._id == SelectedItem._id);
            DoneList.Remove(SelectedItem);
        }
        private ObservableCollection<TodoInfos> _doneList;

        public ObservableCollection<TodoInfos> DoneList
        {
            get { return _doneList; }
            set
            {
                _doneList = value;
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
