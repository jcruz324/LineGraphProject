using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;

using SecurityPricesDesktopApp.Application.Commands;
using SecurityPricesDesktopApp.Application.PriceEngine.Stocks;
using SecurityPricesDesktopApp.Models;

using MicrosoftModel = SecurityPricesDesktopApp.Application.PriceEngine.Stocks.Microsoft;

namespace SecurityPricesDesktopApp.ViewModels
{
    public class SubscriptionListViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private const int TickerMilliseconds = 1000;
        private const int MaxAmountOfPrices = 10;
        private readonly ObservableCollection<SubscriptionModel> _subscriptionModel;
        public SubscriptionModel SelectedSubscriptionModel { get; set; }
        public SubscriptionModel SelectedUnsubscribedSubscriptionModel { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand AddSubscriptionCommand { get; }
        public ICommand CancelSubscriptionCommand { get; set; }
        public IEnumerable<SubscriptionModel> Subscriptions => _subscriptionModel;

        readonly DispatcherTimer timer = new DispatcherTimer();
        public SubscriptionListViewModel()
        {
            _subscriptionModel = new ObservableCollection<SubscriptionModel>();

            _subscriptionModel.Add(new SubscriptionModel(new Amazon() { IsSubscribed = true }));
            _subscriptionModel.Add(new SubscriptionModel(new Apple() { IsSubscribed = true }));
            _subscriptionModel.Add(new SubscriptionModel(new MicrosoftModel() { IsSubscribed = false }));
            
            CancelSubscriptionCommand = new CancelSubscriptionCommand(this);
            AddSubscriptionCommand = new AddSubscriptionCommand(this);
        }

        public SubscriptionListViewModel(ObservableCollection<SubscriptionModel> subscriptionModel)
        {
            _subscriptionModel = subscriptionModel;
            CancelSubscriptionCommand = new CancelSubscriptionCommand(this);
            AddSubscriptionCommand = new AddSubscriptionCommand(this);
            LineCollection.Add(Line1);
            LineCollection.Add(Line2);
            LineCollection.Add(Line3);
            BeginTimerTicker();
        }
        public ObservableCollection<SubscriptionModel> SelectedSubscriptionModels
        {
            get
            {

                return new ObservableCollection<SubscriptionModel>(Subscriptions.Where(x => x.StockTemplate.IsSubscribed));
            }
        }


        public ObservableCollection<SubscriptionModel> AvailableSubscriptionModels
        {
            get
            {
                return new ObservableCollection<SubscriptionModel>(Subscriptions.Where(x => !x.StockTemplate.IsSubscribed));
            }
        }
        
        public new void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region LineSeriesRegion
        public void BeginTimerTicker()
        {
            // Set Ticker method
            timer.Interval = TimeSpan.FromMilliseconds(TickerMilliseconds);
            timer.Tick += TimeChangedUpdateChart;
            timer.Start();
        }

        /// <summary>
        /// Update the chart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TimeChangedUpdateChart(object sender, EventArgs e)
        {
            // X Axis of the Chart is Time
            var timeAxis = DateTime.Now.ToString("HH:mm:ss");

            // Lop off unsubscribed prices
            RemoveUnSubscribedModels();

            RemoveOrphanedLines();

            // If there are no prices then exit
            if (SelectedSubscriptionModels.Count <= 0)
            {
                return;
            }

            // Keep data points to the MaxAmountOfPrices variable or less
            foreach (ObservableCollection<Data> line in LineCollection)
            {
                if (line.Count >= MaxAmountOfPrices)
                    line.RemoveAt(0);
            }

            AddPricesToLines(timeAxis); // Add prices to the chart
        }

        private void AddPricesToLines(string timeAxis)
        {
            // Add Prices on the Data for the line series
            for (int i = 0; i < SelectedSubscriptionModels.Count; i++)
            {
                decimal stockPrice = SelectedSubscriptionModels[i].StockTemplate.StockPrice;
                Thread.Sleep(10);
                string stockTicker = SelectedSubscriptionModels[i].StockTemplate.Ticker;



                foreach (ObservableCollection<Data> line in LineCollection)
                {
                    // Look for stockTicker in the line iteration, if the stock ticker is in the line iteration then add a new price to that line for the Stock Ticker
                    if ((line.ToList().FindIndex(x => x.LineAssignment == stockTicker) >= 0) || (line.Count == 0))
                    {
                        line.Add(new Data() { Col = timeAxis, Price = stockPrice, LineAssignment = stockTicker });
                        break;
                    }
                }
            }
        }

        private void RemoveOrphanedLines()
        {
            // Remove Duplicate Lines off prices. The Lineseries will collapse an existing ticker to the next line, this is to ensure the orphaned line is cleaned up            
            // Todo prevent orphaned lines - I think maybe I have to look into the behavior of the ChartToolKit library
            for (int i = 0; i < SelectedSubscriptionModels.Count; i++)
            {
                string stockTicker = SelectedSubscriptionModels[i].StockTemplate.Ticker;

                foreach (ObservableCollection<Data> line in LineCollection)
                {
                    if (line.ToList().FindIndex(x => x.LineAssignment == stockTicker) >= 0)
                    {
                        // Check other lines 
                        foreach (ObservableCollection<Data> otherLine in LineCollection)
                        {
                            if (!line.Equals(otherLine)) // Don't look at the initial line
                            {
                                if (otherLine.ToList().FindIndex(y => y.LineAssignment == stockTicker) >= 0) // If the same Stock Ticker is found in any other lines then remove the values in the line
                                {
                                    for (int dotIndex = 0; dotIndex < otherLine.Count; dotIndex++)
                                    {
                                        otherLine.RemoveAt(dotIndex); // Dissolve line
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }

        private void RemoveUnSubscribedModels()
        {
            for (int i = 0; i < AvailableSubscriptionModels.Count; i++)
            {
                string stockTicker = AvailableSubscriptionModels[i].StockTemplate.Ticker;
                
                // Loop through each line to see if any line's stock ticker is marked to be removed according to the AvailableSubscriptionModels collection
                foreach (ObservableCollection<Data> line in LineCollection)
                {
                    if (line.ToList().FindIndex(x => x.LineAssignment == stockTicker) >= 0)
                    {
                        for (int dotIndex = 0; dotIndex < line.Count; dotIndex++)
                        {
                            line.RemoveAt(dotIndex);
                        }
                        break; // Line was found, break out of the loop to look for the next Ticker
                    }
                }
            }
        }

        public ObservableCollection<Data> Line1 { get; set; } = new ObservableCollection<Data>();
        public ObservableCollection<Data> Line2 { get; set; } = new ObservableCollection<Data>();
        public ObservableCollection<Data> Line3 { get; set; } = new ObservableCollection<Data>();

        public ObservableCollection<ObservableCollection<Data>> LineCollection = new ObservableCollection<ObservableCollection<Data>>();
 

        public class Data
        {
            public string Col { get; set; }
            public decimal Price { get; set; }
            public string LineAssignment { get; set; }
        }
        #endregion
    }
}




