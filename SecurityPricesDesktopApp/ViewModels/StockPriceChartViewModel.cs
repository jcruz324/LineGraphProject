using System.Windows.Input;

namespace SecurityPricesDesktopApp.ViewModels
{
    public class StockPriceChartViewModel : ViewModelBase // needed to be declared as partial because of the declaration in the corresponding view. 
    {

        private int _floorNumber;

        public int FloorNumber
        {
            get
            {
                return _floorNumber;
            }
            set
            {
                _floorNumber = value;
                OnPropertyChanged(nameof(FloorNumber));
            }

        }
        public ICommand SubscribeCommand { get; }

        public StockPriceChartViewModel()
        {
            
        }

    }
}
