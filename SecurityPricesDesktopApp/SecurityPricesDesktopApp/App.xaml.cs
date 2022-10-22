
using System.Collections.Generic;

using System.Windows;
using SecurityPricesDesktopApp.Models;
using SecurityPricesDesktopApp.Application.PriceEngine.Stocks;
using SecurityPricesDesktopApp.ViewModels;
using MicrosoftModel = SecurityPricesDesktopApp.Application.PriceEngine.Stocks.Microsoft;

namespace SecurityPricesDesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public App()
        {

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow();
            MainWindow.Show();
            base.OnStartup(e);
        }
       
    }
}
