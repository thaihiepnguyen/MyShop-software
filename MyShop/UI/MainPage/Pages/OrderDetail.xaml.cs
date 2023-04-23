using DocumentFormat.OpenXml.VariantTypes;
using MyShop.BUS;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for OrderDetail.xaml
    /// </summary>
    public partial class OrderDetail : Page
    {
        Frame _pageNavigation;
        private OrderBUS _orderBUS;
        private CustomerBUS _customerBUS;
        private ObservableCollection<Data> _list;


        class Resources
        {
            public string FirstIcon { get; set; }
            public string LastIcon { get; set; }
            public string NextIcon { get; set; }
            public string PrevIcon { get; set; }
        }


        public class Data
        {
            public int OrderID { get; set; }
            public DateTime CreateAt { get; set; }
            public string CusName { get; set; }
            public string FinalTotal { get; set; }

            public Data(ShopOrderDTO list, CustomerBUS customer)
            {

                customer = new CustomerBUS();
                OrderID = list.OrderID;
                CreateAt = list.CreateAt.Date;
                CusName = customer.getNameById(list.CusID);
                FinalTotal = string.Format("{0:N0} đ", list.FinalTotal);
            }
        }

        public OrderDetail(Frame pageNavigation)
        {

            InitializeComponent();
            _customerBUS = new CustomerBUS();
            ObservableCollection<Data> list = new ObservableCollection<Data>();
            _pageNavigation = pageNavigation;
            _orderBUS = new OrderBUS();
            var orders = _orderBUS.getAllShopOrder();

            foreach (var order in orders)
            {
                list.Add(new Data(order, _customerBUS));
            }
            productsListView.ItemsSource = list;

            _list = list;
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            _pageNavigation.NavigationService.Navigate(new AddOrder(_pageNavigation, _list));
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void FirstButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LastButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new Resources()
            {
                FirstIcon = "Assets/Images/ic-first.png",
                LastIcon = "Assets/Images/ic-last.png",
                NextIcon = "Assets/Images/ic-next.png",
                PrevIcon = "Assets/Images/ic-prev.png"
            };
        }
    }
}