using SecurityPricesDesktopApp.Models;
using System.Collections.Generic;

namespace SecurityPricesDesktopApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; }

        public MainViewModel(IList<SubscriptionModel> subscriptionModels)
        {

        }
    }
}
