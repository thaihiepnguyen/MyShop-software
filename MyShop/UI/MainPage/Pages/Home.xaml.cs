using MyShop.BUS;
using MyShop.DAO;
using MyShop.DTO;
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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        private List<ProductDTO>? _products = null;
        private string _currentKey = "";
        private int _currentPage = 1;
        private int _rowsPerPage = 9;
        private int _totalItems = 0;
        private int _totalPages = 0;

        class Resources
        {
            public string FirstIcon { get; set; }
            public string LastIcon { get; set; }
            public string NextIcon { get; set; }
            public string PrevIcon { get; set; }
        }


        // Mục đích là đổ dữ liệu của Class này lên UI
        class Data
        {
            public string? ProName { get; set; }
            public string? ProImage { get; set; }
            public string? CatIcon { get; set; }
            public string? CatName { get; set; }
            public decimal? Price { get; set; }

            public Data(ProductDTO productDTO)
            {
                ProName = productDTO.ProName;
                ProImage = productDTO.Image_path;
                Price = productDTO.Price;

                if (productDTO.Cat_ID == 1)
                {
                    CatIcon = "Android";
                    CatName = "Android";
                } else if (productDTO.Cat_ID == 2)
                {
                    CatIcon = "Apple";
                    CatName = "Iphone";
                } else
                {

                }
            }
        }


        public Home()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            updateDataSource();
            updatePagingInfo();
            this.DataContext = new Resources()
            {
                FirstIcon = "Assets/Images/ic-first.png",
                LastIcon = "Assets/Images/ic-last.png",
                NextIcon = "Assets/Images/ic-next.png",
                PrevIcon = "Assets/Images/ic-prev.png"
            };
        }

        private void updateDataSource(int page = 1, string keyword = "")
        {
            List<Data> list = new List<Data>();
            _currentPage = page;
            ProductBUS productBUS = new ProductBUS();
            (_products, _totalItems) = productBUS.findProductBySearch(_currentPage, _rowsPerPage, keyword);

            foreach (var product in _products)
            {
                list.Add(new Data(product));
            }

            dataListView.ItemsSource = list;

            infoTextBlock.Text = $"Đang hiển thị {_products.Count} trên tổng số {_totalItems} sản phẩm";
        }

        private void updatePagingInfo()
        {
            _totalPages = _totalItems / _rowsPerPage +
                   (_totalItems % _rowsPerPage == 0 ? 0 : 1);

            // Cập nhật ComboBox
            var lines = new List<Tuple<int, int>>();
            for (int i = 1; i <= _totalPages; i++)
            {
                lines.Add(new Tuple<int, int>(i, _totalPages));
            }

            pageInfoTextBlock.Text = $"{_currentPage}/{_totalPages}";
        }

        private void SearchTermTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string keyword = SearchTermTextBox.Text;
                _currentKey = keyword;

                updateDataSource(1, keyword);
                updatePagingInfo();
            }
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1) {
                _currentPage--;
                updateDataSource(_currentPage, _currentKey);
                updatePagingInfo();
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                updateDataSource(_currentPage, _currentKey);
                updatePagingInfo();
            }
        }

        private void FirstButton_Click(object sender, RoutedEventArgs e)
        {
            updateDataSource(1, _currentKey);
            updatePagingInfo();
        }

        private void LastButton_Click(object sender, RoutedEventArgs e)
        {
            updateDataSource(_totalPages, _currentKey);
            updatePagingInfo();
        }
    }
}
