using MyShop.BUS;
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
using System.Collections.ObjectModel;
using System.Xml.Schema;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.VariantTypes;

namespace MyShop.UI.MainPage.Pages
{
    /// <summary>
    /// Interaction logic for AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Page
    {
        Frame _pageNavigation;
        private ProductBUS _productBUS;
        private CustomerBUS _customerBUS;
        private OrderBUS _orderBUS;
        private ObservableCollection<CustomerDTO> _customers;
        private bool _verifyOrder = false;
        private int _orderID = 0;
        private ObservableCollection<Data> _data;
        private int _currentCustomerID;
        private ProductDTO _currentProduct;
        private ObservableCollection<ProductDTO> _products;
        private Decimal _currentTotalPrice = 0;
        private ShopOrderDTO _shopOrderDTO;

        public class Data {
            public string ProName { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public decimal TotalPrice { get; set; }
        }



        public AddOrder(Frame pageNavigation)
        {
            _pageNavigation = pageNavigation;
            _productBUS = new ProductBUS();
            _customerBUS = new CustomerBUS();
            _orderBUS = new OrderBUS();
            _data = new ObservableCollection<Data>();
            InitializeComponent();
        }

        private void SaveOrder_Click(object sender, RoutedEventArgs e)
        {
            _orderBUS.patchShopOrder(_shopOrderDTO);

            MessageBox.Show("Đã lưu đơn hàng thành công", "Thông Báo");
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _pageNavigation.NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var products = _productBUS.getAll();
            _products = products;
            var customers = _customerBUS.getAll();
            _customers = customers;
            ProductCombobox.ItemsSource = products;
            _currentProduct = products[0];
            ProductCombobox.SelectedIndex = 0;

            CustomerCombobox.ItemsSource = customers;
            CustomerCombobox.SelectedIndex = 0;

            this.DataContext = _currentProduct;
            FinalPrice.Text = string.Format("{0:N0} đ", _currentTotalPrice);
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {

            if (QuantityTermTextBox.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số lượng");
                return;
            }

            var purchareDTO = new PurchaseDTO();
            var orderDetailDTO = new ShopOrderDTO();

            var customerDTO = (CustomerDTO)CustomerCombobox.SelectedValue;
            
            var productDTO = (ProductDTO)ProductCombobox.SelectedValue;
            int quantity = int.Parse(QuantityTermTextBox.Text);

            orderDetailDTO.CusID = customerDTO.CusID;
            orderDetailDTO.CreateAt = DateTime.Now.Date;

            if (_verifyOrder && (customerDTO.CusID != _currentCustomerID))
            {
                MessageBox.Show("Vui lòng hoàn thiện đơn hàng", "Thông báo", MessageBoxButton.OK);
                return;
            }

            if (!_verifyOrder)
            {
                _orderID = _orderBUS.addShopOrder(orderDetailDTO);
            }

            orderDetailDTO.OrderID = _orderID;
            purchareDTO.OrderID = _orderID;
            purchareDTO.ProID = productDTO.ProId;
            purchareDTO.Quantity = quantity;
            purchareDTO.TotalPrice = productDTO.Price * quantity;

            _orderBUS.addPurchase(purchareDTO);

            MessageBox.Show("Sản phẩm đã thêm thành công", "Thông báo", MessageBoxButton.OK);

            var data = new Data();
            data.Quantity = quantity;
            data.Price = productDTO.Price;
            data.ProName = productDTO.ProName;
            data.TotalPrice = productDTO.Price * quantity;

            _currentTotalPrice += data.TotalPrice;

            _data.Add(data);

            ordersListView.ItemsSource = _data;

            _verifyOrder = true;
            _currentCustomerID = customerDTO.CusID;

            FinalPrice.Text = string.Format("{0:N0} đ", _currentTotalPrice);
            _shopOrderDTO = orderDetailDTO;
            _shopOrderDTO.FinalTotal = _currentTotalPrice;
        }

        private void SaveCustomer_Click(object sender, RoutedEventArgs e)
        {
            var customerDTO = new CustomerDTO();

            int CusID;


            customerDTO.CusName = CustomerTermTextBox.Text;
            CusID = _customerBUS.addCustomer(customerDTO);
            customerDTO.CusID = CusID;

            _customers.Add(customerDTO);

            MessageBox.Show("Khách hàng đã thêm thành công", "Thông báo", MessageBoxButton.OK);
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void ProductCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ProductCombobox.SelectedIndex;

            _currentProduct.copy(_products[index]);
        }
    }
}
