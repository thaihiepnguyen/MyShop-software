using System;
using System.ComponentModel;
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
        private CategoryDTO _category;
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

            CategoryDTO category = _categoryBUS.getCategoryById(_product.CatID);
            _category = category;
            Data data = new(_product, category);
            
            DataContext = data;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Lưu lại trạng thái trước đó 
            var (key, page, currency, startPrice, endPrice) = _home.getCurrentState();

            _pageNavigation.NavigationService.Navigate(new Home(_pageNavigation, page, currency, key, startPrice, endPrice));
        }

        private void DelProduct_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult choice = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông Báo", MessageBoxButton.OKCancel);

            if (choice == MessageBoxResult.OK)
            {
                // lưu lại trạng thái trước đó
                var (key, page, currency, startPrice, endPrice) = _home.getCurrentState();
                _productBUS.delProduct(_product.ProId);
                _pageNavigation.NavigationService.Navigate(new Home(_pageNavigation, page, currency, key, startPrice, endPrice));
            } else
            {

            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var clonedProduct = (ProductDTO)_product.Clone();


            _pageNavigation.NavigationService.Navigate(new UpdateProduct(_product, _category, _pageNavigation));
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
