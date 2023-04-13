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
        private CategoryBUS _categoryBUS;
        private Home _home;

        // Mục đích là đổ dữ liệu của Class này lên UI
        class Data
        {
            public string? ProName { get; set; }
            public double Ram { get; set; }
            public int Rom { get; set; }
            public double ScreenSize { get; set; }
            public string? TinyDes { get; set; }
            public string? FullDes { get; set; }
            public decimal Price { get; set; }
            public string? ImagePath { get; set; }
            public string? Trademark { get; set; }
            public int BatteryCapacity { get; set; }
            public int Quantity { get; set; }
            public int? Block { get; set; }
            public int? CatID { get; set; }

            public string? CatName { get; set; }

            public string? CatIcon { get; set; }


            public Data(ProductDTO productDTO, CategoryDTO categoryDTO)
            {
                ProName = productDTO.ProName;
                ImagePath = productDTO.ImagePath;
                Price = productDTO.Price;
                Rom = productDTO.Rom;
                Ram = productDTO.Ram;
                ScreenSize = productDTO.ScreenSize;
                TinyDes = productDTO.TinyDes;
                FullDes = productDTO.FullDes;
                Price = productDTO.Price;
                TinyDes = productDTO.TinyDes;
                FullDes = productDTO.FullDes;
                Quantity= productDTO.Quantity;
                BatteryCapacity = productDTO.BatteryCapacity;
                Block = productDTO.Block;
                CatID = productDTO.CatID;
                CatName = categoryDTO.CatName;

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

            Data data = new Data(_product, category);
            
            DataContext = data;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _pageNavigation.NavigationService.Navigate(_home);
        }

        private void DelProduct_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult choice = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông Báo", MessageBoxButton.OKCancel);

            if (choice == MessageBoxResult.OK)
            {
                _productBUS.delProduct(_product.ProId);
                _pageNavigation.NavigationService.Navigate(_home);
            } else
            {

            }
        }
    }
}
