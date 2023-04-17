﻿using Microsoft.Win32;
using MyShop.BUS;
using MyShop.DAO;
using MyShop.DTO;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
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
        private CategoryDTO _categoryDTO;
        public UpdateProduct(ProductDTO product, CategoryDTO categoryDTO, Frame pageNavigation)
        {
            InitializeComponent();

            _pageNavegation = pageNavigation;
            _categoryBUS = new CategoryBUS();
            _productBUS = new ProductBUS();

            var categories = _categoryBUS.getAll();


            CategoryCombobox.ItemsSource = categories;

            CategoryCombobox.SelectedIndex = (int)(product.CatID - 1);
            _productDTO = product;
            _categoryDTO = categoryDTO;
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
            int id = _productDTO.ProId;
            _productDTO.ProId = id;
            _productDTO.ProName = NameTermTextBox.Text;
            _productDTO.Ram = Double.Parse(RamTermTextBox.Text);
            _productDTO.Rom = int.Parse(RomTermTextBox.Text);
            _productDTO.ScreenSize = Double.Parse(ScreenSizeTermTextBox.Text);
            _productDTO.TinyDes = DesTermTextBox.Text;
            _productDTO.Price = Decimal.Parse(PriceTermTextBox.Text);
            _productDTO.Trademark = TradeMarkTermTextBox.Text;
            _productDTO.BatteryCapacity = int.Parse(PinTermTextBox.Text);
            _productDTO.CatID = CategoryCombobox.SelectedIndex + 1;

            var categoryTemp = _categoryBUS.getCategoryById(_productDTO.CatID);
            _categoryDTO.CatID = categoryTemp.CatID;
            _categoryDTO.CatName = categoryTemp.CatName;
            _categoryDTO.CatIcon = categoryTemp.CatIcon;

            _productDTO.Quantity = int.Parse(QuantityTermTextBox.Text);
            _productDTO.Block = 0;

            _productBUS.patchProduct(_productDTO);

            string key = Guid.NewGuid().ToString();

            if (_isImageChanged)
                _productBUS.uploadImage(_selectedImage, id, key);

            MessageBox.Show("Sản phẩm đã chỉnh sửa thành công", "Thông báo", MessageBoxButton.OK);
            _pageNavegation.NavigationService.GoBack();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _pageNavegation.NavigationService.GoBack();
        }
    }
}
