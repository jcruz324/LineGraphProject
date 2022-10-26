using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityPricesDesktopApp.Models;
using SecurityPricesDesktopApp.ViewModels;

namespace SecurityPricesDesktopApp.Application.Commands
{
    public class AddSubscriptionCommand : CommandBase
    {
        private readonly SubscriptionListViewModel _viewModel;

        public AddSubscriptionCommand(SubscriptionListViewModel model)
        {
            _viewModel = model;
            
        }
        public override void Execute(object parameter)
        {
            // Todo - check to ensure that ticker is selected, maybe disable the button
            _viewModel.SelectedUnsubscribedSubscriptionModel.StockTemplate.IsSubscribed = true;
            _viewModel.OnPropertyChanged("SelectedSubscriptionModels");
            _viewModel.OnPropertyChanged("AvailableSubscriptionModels");
        }
    }
}
