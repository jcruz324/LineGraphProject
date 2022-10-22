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
           // Todo store the Line collections into a list to handle all of these behaviors
            var xAxisValue = DateTime.Now.ToString("HH:mm:ss");

            // Lop off unsubscribed prices
            RemoveUnSubscribedModels();

            
            RemoveOrphanedLines();
            // If there are no prices then exit
            if (SelectedSubscriptionModels.Count <= 0)
            {
                return;
            }

            // Keep data points to 10 prices or less
            if (Line1.Count >= MaxAmountOfPrices)
            {
                Line1.RemoveAt(0);
            }
            if (Line2.Count >= MaxAmountOfPrices)
            {
                Line2.RemoveAt(0);
            }
            if (Line3.Count >= MaxAmountOfPrices)
            {
                Line3.RemoveAt(0);
            }
            
          
            // Add Prices on the Data for the line series
            for (int i = 0; i < SelectedSubscriptionModels.Count; i++)
            {
                decimal stockPrice = SelectedSubscriptionModels[i].StockTemplate.StockPrice;
                Thread.Sleep(10);
                string stockTicker = SelectedSubscriptionModels[i].StockTemplate.Ticker;
               

                if ((Line1.ToList().FindIndex(line => line.LineAssignment == stockTicker) >= 0) || (Line1.Count == 0))
                {
                    Line1.Add(new Data() { Col = xAxisValue, Price = stockPrice, LineAssignment = stockTicker });
                    continue;
                }

                if ((Line2.ToList().FindIndex(line => line.LineAssignment == stockTicker) >= 0) || (Line2.Count == 0))
                {
                    Line2.Add(new Data() { Col = xAxisValue, Price = stockPrice, LineAssignment = stockTicker });
                    continue;
                }

                if ((Line3.ToList().FindIndex(line => line.LineAssignment == stockTicker) >= 0) || (Line3.Count == 0))
                {
                    Line3.Add(new Data() { Col = xAxisValue, Price = stockPrice, LineAssignment = stockTicker });
                }

            }
        }

        private void RemoveOrphanedLines()
        {
            // Remove Duplicate Lines off prices. The Lineseries will collapse an existing ticker to the next line, this is to ensure the orphaned line is cleaned up
            // Todo prevent orphaned lines
            for (int i = 0; i < SelectedSubscriptionModels.Count; i++)
            {
                string stockTicker = SelectedSubscriptionModels[i].StockTemplate.Ticker;

                if ((Line2.ToList().FindIndex(line => line.LineAssignment == stockTicker) >= 0) &&
                    (Line1.ToList().FindIndex(line => line.LineAssignment == stockTicker) >= 0))
                {
                    for (int dotIndex = 0; dotIndex < Line2.Count; dotIndex++)
                    {
                        Line2.RemoveAt(dotIndex);
                    }
                }

                if ((Line3.ToList().FindIndex(line => line.LineAssignment == stockTicker) >= 0) &&
                    (Line2.ToList().FindIndex(line => line.LineAssignment == stockTicker) >= 0))
                {
                    for (int dotIndex = 0; dotIndex < Line3.Count; dotIndex++)
                    {
                        Line3.RemoveAt(dotIndex);
                    }
                }
            }
        }

        private void RemoveUnSubscribedModels()
        {
            for (int i = 0; i < AvailableSubscriptionModels.Count; i++)
            {
                string stockTicker = AvailableSubscriptionModels[i].StockTemplate.Ticker;

                if (Line1.ToList().FindIndex(line => line.LineAssignment == stockTicker) >= 0)
                {
                    for (int dotIndex = 0; dotIndex < Line1.Count; dotIndex++)
                    {
                        Line1.RemoveAt(dotIndex);
                    }
                }

                if (Line2.ToList().FindIndex(line => line.LineAssignment == stockTicker) >= 0)
                {
                    for (int dotIndex = 0; dotIndex < Line2.Count; dotIndex++)
                    {
                        Line2.RemoveAt(dotIndex);
                    }
                }

                if (Line3.ToList().FindIndex(line => line.LineAssignment == stockTicker) >= 0)
                {
                    for (int dotIndex = 0; dotIndex < Line3.Count; dotIndex++)
                    {
                        Line3.RemoveAt(dotIndex);
                    }
                }
            }
        }

        public ObservableCollection<Data> Line1 { get; set; } = new ObservableCollection<Data>();
        public ObservableCollection<Data> Line2 { get; set; } = new ObservableCollection<Data>();
        public ObservableCollection<Data> Line3 { get; set; } = new ObservableCollection<Data>();

        public class Data
        {
            public string Col { get; set; }
            public decimal Price { get; set; }
            public string LineAssignment { get; set; }
        }


        #endregion
    }
}




