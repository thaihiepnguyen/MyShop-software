﻿using MyShop.UI.MainPage;
using MyShop.UI.MainPage.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Threading.Tasks;

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

            pageNavigation.NavigationService.Navigate(new DashBoard());
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

            // reset border trước khi click
            resetBorder();


            var addedContainer = ListOfItems.ItemContainerGenerator.ContainerFromItem(e.AddedItems[0]) as ListViewItem;
            if (addedContainer != null)
            {
                var border = FindVisualChild<Border>(addedContainer);
                if (border != null)
                {
                    border.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    border.BorderBrush = new SolidColorBrush(Colors.DarkOrange);
                    border.CornerRadius = new CornerRadius(20);
                    border.BorderThickness = new Thickness(1);
                    border.Width = 140;
                }
            }


            if (e.RemovedItems.Count > 0)
            {
                var removedContainer = ListOfItems.ItemContainerGenerator.ContainerFromItem(e.RemovedItems[0]) as ListViewItem;
                if (removedContainer != null)
                {
                    var border = FindVisualChild<Border>(removedContainer);
                    if (border != null)
                    {
                        border.Background = new SolidColorBrush(Colors.Transparent);
                        border.BorderThickness = new Thickness(0);
                    }
                }
            }
        }

        // Tìm trong parent. Nếu có kiểu dữ liệu T thì trả ra T còn không thì thôi.
        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T result)
                {
                    return result;
                }
                else
                {
                    T childResult = FindVisualChild<T>(child);
                    if (childResult != null)
                        return childResult;
                }
            }
            return null;
        }

        async private void ListOfItems_Loaded(object sender, RoutedEventArgs e)
        {
            if (ListOfItems.Items.Count > 0)
            {
                // Phải chờ cho nó load xong thì mới gán border được haha
                await Task.Delay(100);
                var container = ListOfItems.ItemContainerGenerator.ContainerFromIndex(0) as ListViewItem;
                if (container != null)
                {
                    var border = FindVisualChild<Border>(container);
                    if (border != null)
                    {
                        border.Background = new SolidColorBrush(Colors.WhiteSmoke);
                        border.BorderBrush = new SolidColorBrush(Colors.DarkOrange);
                        border.CornerRadius = new CornerRadius(20);
                        border.BorderThickness = new Thickness(1);
                        border.Width = 140;
                    }
                }
            }
        }

        private void resetBorder()
        {
            foreach(var item in ListOfItems.Items)
            {
                var container = ListOfItems.ItemContainerGenerator.ContainerFromItem(item) as ListViewItem;
                if (container != null)
                {
                    var border = FindVisualChild<Border>(container);
                    if (border != null)
                    {
                        border.Background = new SolidColorBrush(Colors.Transparent);
                        border.BorderThickness = new Thickness(0);
                    }
                }
            }
        }
    }
}
