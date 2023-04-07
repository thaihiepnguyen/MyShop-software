using MyShop.UI.MainPage;
using MyShop.UI.MainPage.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MyShop.UI.MainPage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        class Resoures
        {
            public string MainBgPath { get; set; }
            public string CloseIconPath { get; set; }
            public string MinimizeIconPath { get; set; }
            public string Logo { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        ObservableCollection<Item> Items = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Load Nav Background
            this.DataContext = new Resoures()
            {
                MainBgPath = "Assets/Images/main-bg.png",
                CloseIconPath = "Assets/Images/close-icon.png",
                MinimizeIconPath = "Assets/Images/minimize-icon.png",
                Logo = "Assets/Images/logo.png"
            };

            Items = new ObservableCollection<Item>()
            {
                new Item()
                {
                    FontIcon = "Dashboard",
                    ItemName = "Dashboard"
                },
                new Item()
                {
                    FontIcon = "Home",
                    ItemName = "Home"
                },
                new Item()
                {
                    FontIcon = "Heart",
                    ItemName = "Favorite"
                },
                new Item()
                {
                    FontIcon = "ShoppingCart",
                    ItemName = "Order",
                },
                new Item()
                {
                    FontIcon = "InfoCircle",
                    ItemName = "About us"
                } 

            };

            ListOfItems.ItemsSource = Items;

        }

        private void ListOfItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedIndex = ListOfItems.SelectedIndex;

            if (selectedIndex == 0)
            {
                pageNavigation.NavigationService.Navigate(new DashBoard());
            }
            if (selectedIndex == 1)
            {
                pageNavigation.NavigationService.Navigate(new Home());
            }
            if (selectedIndex == 2)
            {
                pageNavigation.NavigationService.Navigate(new Favorite());
            }
            if (selectedIndex == 3)
            {
                pageNavigation.NavigationService.Navigate(new Order());
            }
            if (selectedIndex == 4)
            {
                pageNavigation.NavigationService.Navigate(new AboutUs());
            }
        }
    }
}
