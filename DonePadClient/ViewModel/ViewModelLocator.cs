/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:DonePadClient"
                           x:Key="Locator" />
  </Application.Resources>

  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using DonePadClient.Models;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace DonePadClient.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}
            //×¢²áviewmodel
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<TodoViewModel>();
            SimpleIoc.Default.Register<AddTodoViewModel>();
            SimpleIoc.Default.Register<DoneViewModel>();
            SimpleIoc.Default.Register<RegisterViewModel>();
            SimpleIoc.Default.Register<UserInfoViewModel>();
            //×¢²ámodel
            SimpleIoc.Default.Register<User>();
            SimpleIoc.Default.Register<TodoInfos>();

            SimpleIoc.Default.Register<users>();
            SimpleIoc.Default.Register<todoinfo>();
            //×¢²áview
            SimpleIoc.Default.Register<LoginWindow>();
            SimpleIoc.Default.Register<MainWindow>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public LoginViewModel Login
        {
            get { return ServiceLocator.Current.GetInstance<LoginViewModel>(); }
        }

        public TodoViewModel Todo
        {
            get { return ServiceLocator.Current.GetInstance<TodoViewModel>(); }
        }

        public AddTodoViewModel AddTodo
        {
            get { return ServiceLocator.Current.GetInstance<AddTodoViewModel>(); }
        }

        public DoneViewModel Done
        {
            get { return ServiceLocator.Current.GetInstance<DoneViewModel>(); }
        }

        public RegisterViewModel Register
        {
            get { return ServiceLocator.Current.GetInstance<RegisterViewModel>(); }
        }

        public UserInfoViewModel UserInfo
        {
            get { return ServiceLocator.Current.GetInstance<UserInfoViewModel>(); }
        }
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}