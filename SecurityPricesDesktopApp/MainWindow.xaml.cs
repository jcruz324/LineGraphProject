using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using SecurityPricesDesktopApp.ViewModels;
using SecurityPricesDesktopApp.Views;

namespace SecurityPricesDesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ViewModelBase();
        }

        private void OpenChartPrices_Click(object sender, RoutedEventArgs e)
        {

            SubscriptionView subscriptionView = new SubscriptionView();
            subscriptionView.Show();

        }

    }
}
