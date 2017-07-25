using Microsoft.Practices.ServiceLocation;

namespace DonePadClient.ViewModel
{
    public class ViewModelBase : GalaSoft.MvvmLight.ViewModelBase
    {
        public T GetInstance<T>()
        {
           return ServiceLocator.Current.GetInstance<T>(typeof(T).Name);
        }
    }
}