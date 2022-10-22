using System.Windows;
using System.Collections.ObjectModel;

using SecurityPricesDesktopApp.Application.PriceEngine.Stocks;
using SecurityPricesDesktopApp.Models;
using SecurityPricesDesktopApp.ViewModels;
using MicrosoftModel = SecurityPricesDesktopApp.Application.PriceEngine.Stocks.Microsoft;

namespace SecurityPricesDesktopApp.Views
{
    /// <summary>
    /// Interaction logic for SubscriptionView.xaml
    /// </summary>
    public partial class SubscriptionView : Window
    {
        public SubscriptionView()
        {
            InitializeComponent();

            // Inject stock data
            DataContext = new SubscriptionListViewModel(new ObservableCollection<SubscriptionModel>()
            {
                new SubscriptionModel(new Amazon() { IsSubscribed = true }),
                new SubscriptionModel(new Apple() { IsSubscribed = false }),
                new SubscriptionModel(new MicrosoftModel() { IsSubscribed = true })
            });
            
        }

    }

}
