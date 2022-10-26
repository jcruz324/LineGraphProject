using System.Windows.Input;

namespace SecurityPricesDesktopApp.ViewModels
{
    public class StockPriceChartViewModel : ViewModelBase // needed to be declared as partial because of the declaration in the corresponding view. 
    {

        public ICommand SubscribeCommand { get; }

        public StockPriceChartViewModel()
        {
            
        }

    }
}
