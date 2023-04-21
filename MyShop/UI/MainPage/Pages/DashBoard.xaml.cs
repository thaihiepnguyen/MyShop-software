using MyShop.BUS;
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

namespace MyShop.UI.MainPage.Pages
{
    /// <summary>
    /// Interaction logic for DashBoard.xaml
    /// </summary>
    class Resources
    {
        public string ProductTotalBG { get; set; }
        public string OrderTotalBG { get; set; }
        public int TotalProduct { get; set; }
    }


    public partial class DashBoard : Page
    {
        ProductBUS _productBUS;

        public DashBoard()
        {
            _productBUS = new ProductBUS();
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            int total = _productBUS.countTotalProduct();

            var top5Product = _productBUS.getTop5Product();


            this.DataContext = new Resources()
            {
                ProductTotalBG = "Assets/Images/item1-bg.jpg",
                OrderTotalBG = "Assets/Images/item2-bg.jpg",
                TotalProduct = total
            };

            productsListView.ItemsSource = top5Product;
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
