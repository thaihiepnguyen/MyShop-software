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
            List<Data> list = new List<Data>(); 

            ProductBUS productBUS = new ProductBUS();

            var products = productBUS.getProducts();

            foreach (var product in products)
            {
                list.Add(new Data(product));
            }

            dataListView.ItemsSource = list;
        }
    }
}
