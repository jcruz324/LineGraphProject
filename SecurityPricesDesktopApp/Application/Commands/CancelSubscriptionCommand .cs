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
    public class CancelSubscriptionCommand : CommandBase
    {
        private SubscriptionListViewModel _viewModel;
        public CancelSubscriptionCommand(SubscriptionListViewModel viewModel)
        {
            _viewModel = viewModel;
            
        }

        public override void Execute(object parameter)
        {
            // Todo - check to ensure that ticker is selected, maybe disable the button for the removal
            _viewModel.SelectedSubscriptionModel.StockTemplate.IsSubscribed = false;
            _viewModel.OnPropertyChanged("SelectedSubscriptionModels");
            _viewModel.OnPropertyChanged("AvailableSubscriptionModels");
        }
    }
}
