using System.ComponentModel;
using System.Windows.Input;
using SecurityPricesDesktopApp.Application;
using SecurityPricesDesktopApp.Application.Commands;

namespace SecurityPricesDesktopApp.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}