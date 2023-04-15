using Microsoft.Win32;
using MyShop.BUS;
using MyShop.DAO;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for UpdateProduct.xaml
    /// </summary>
    public partial class UpdateProduct : Page
    {
        private bool _isImageChanged = false;
        private FileInfo? _selectedImage = null;
        private CategoryBUS _categoryBUS;
        private ProductBUS _productBUS;
        private Frame _pageNavegation;
        private ProductDTO _productDTO;
        public UpdateProduct(ProductDTO product, Frame pageNavigation)
        {
            InitializeComponent();

            _pageNavegation = pageNavigation;
            _categoryBUS = new CategoryBUS();
            _productBUS = new ProductBUS();

            var categories = _categoryBUS.getAll();


            CategoryCombobox.ItemsSource = categories;

            CategoryCombobox.SelectedIndex = (int)(product.CatID - 1);
            _productDTO = product;
            DataContext = product;
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            screen.Filter = "Files|*.png; *.jpg; *.jpeg;";
            if (screen.ShowDialog() == true)
            {
                _isImageChanged = true;
                _selectedImage = new FileInfo(screen.FileName);

                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(screen.FileName, UriKind.Absolute);
                bitmap.EndInit();

                AddedImage.Source = bitmap;
            }
        }

        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            var productDTO = new ProductDTO();

            int id = _productDTO.ProId;
            productDTO.ProId = id;
            productDTO.ProName = NameTermTextBox.Text;
            productDTO.Ram = Double.Parse(RamTermTextBox.Text);
            productDTO.Rom = int.Parse(RomTermTextBox.Text);
            productDTO.ScreenSize = Double.Parse(ScreenSizeTermTextBox.Text);
            productDTO.TinyDes = DesTermTextBox.Text;
            productDTO.Price = Decimal.Parse(PriceTermTextBox.Text);
            productDTO.Trademark = TradeMarkTermTextBox.Text;
            productDTO.BatteryCapacity = int.Parse(PinTermTextBox.Text);
            productDTO.CatID = CategoryCombobox.SelectedIndex + 1;
            productDTO.Quantity = int.Parse(QuantityTermTextBox.Text);
            productDTO.Block = 0;

            _productBUS.patchProduct(productDTO);

            string key = Guid.NewGuid().ToString();

            if (_isImageChanged)
                _productBUS.uploadImage(_selectedImage, id, key);

            MessageBox.Show("Sản phẩm đã chỉnh sửa thành công", "Thông báo", MessageBoxButton.OK);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _pageNavegation.NavigationService.GoBack();
        }
    }
}
