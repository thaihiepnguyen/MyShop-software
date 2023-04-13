using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MyShop.BUS;
using MyShop.DTO;

namespace MyShop.UI.MainPage.Pages
{
    /// <summary>
    /// Interaction logic for ProductDetail.xaml
    /// </summary>
    public partial class ProductDetail : Page
    {
        private ProductDTO _product;
        private Frame _pageNavigation;
        private ProductBUS _productBUS;
        public ProductDetail(ProductDTO product, Frame pageNavigation)
        {
            InitializeComponent();
            _product = product;
            _pageNavigation = pageNavigation;
            _productBUS = new ProductBUS();

            DataContext = _product;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _pageNavigation.NavigationService.Navigate(new Home(_pageNavigation));
        }

        private void DelProduct_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult choice = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông Báo", MessageBoxButton.OKCancel);

            if (choice == MessageBoxResult.OK)
            {
                _productBUS.delProduct(_product.ProId);
                _pageNavigation.NavigationService.Navigate(new Home(_pageNavigation));
            } else
            {

            }
        }
    }
}
