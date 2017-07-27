using DonePadClient.Command;
using DonePadClient.View;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;

namespace DonePadClient.ViewModel
{
    public class ViewModelBase : GalaSoft.MvvmLight.ViewModelBase
    {
        public T GetInstance<T>()
        {
            return ServiceLocator.Current.GetInstance<T>(typeof(T).Name);
        }

        public void NotifyToWindow(NotifyCommand command)
        {
            Messenger.Default.Send(new NotificationMessage(this, command.ToString()));
        }
    }
}