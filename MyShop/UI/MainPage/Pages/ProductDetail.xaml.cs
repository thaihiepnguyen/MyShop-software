using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private CategoryBUS _categoryBUS;
        private Home _home;

        // Mục đích là đổ dữ liệu của Class này lên UI
        class Data : INotifyPropertyChanged
        {
            public ProductDTO Product { get; set; }
            public CategoryDTO Category { get; set; }

            public string CatIcon { get; set; }


            public Data(ProductDTO productDTO, CategoryDTO categoryDTO)
            {
                this.Product = productDTO;
                this.Category = categoryDTO;

                if (productDTO.CatID == 1)
                {
                    CatIcon = "Android";
                }
                else if (productDTO.CatID == 2)
                {
                    CatIcon = "Apple";
                }
                else
                {

                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;
        }

        public ProductDetail(Home home, ProductDTO product, Frame pageNavigation)
        {
            InitializeComponent();
            _home = home;
            _product = product;
            _pageNavigation = pageNavigation;
            _productBUS = new ProductBUS();
            _categoryBUS = new CategoryBUS();

            CategoryDTO category = _categoryBUS.getCategoryById((int)_product.CatID);

            Data data = new(_product, category);
            
            DataContext = data;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Lưu lại trạng thái trước đó 
            var (key, page, startPrice, endPrice) = _home.getCurrentState();

            _pageNavigation.NavigationService.Navigate(new Home(_pageNavigation, page, key, startPrice, endPrice));
        }

        private void DelProduct_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult choice = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông Báo", MessageBoxButton.OKCancel);

            if (choice == MessageBoxResult.OK)
            {
                // lưu lại trạng thái trước đó
                var (key, page, startPrice, endPrice) = _home.getCurrentState();
                _productBUS.delProduct(_product.ProId);
                _pageNavigation.NavigationService.Navigate(new Home(_pageNavigation, page, key, startPrice, endPrice));
            } else
            {

            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var clonedProduct = (ProductDTO)_product.Clone();


            _pageNavigation.NavigationService.Navigate(new UpdateProduct(_product, _pageNavigation));
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
