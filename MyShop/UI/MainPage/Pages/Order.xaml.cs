

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace MyShop.UI.MainPage.Pages
{
    public partial class Order : Page
    {
        class SmartPhone 
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string Price { get; set; }
            public string AddressOrder { get; set; }

            // không nên để tên biến như này nhé!. Tất cả biến bên dưới là properties
            // biến private thực sự bị dấu đi rồi.
            // TODO: THAM KHẢO - https://docs.google.com/presentation/d/1x8LbCQq5hPI786MQilbk5cgx88Z0vji5/edit#slide=id.p13
            public string imagePath { get; set; }
            public string screenSize { get; set; }
            public string trademark { get; set; }
            public string batteryCapacity { get; set; }
            public string ram { get; set; }

            public string rom { get; set; }
            public string tinyDes { get; set; }
            public string fullDes { get; set; }
            public string price { get; set; }
            public string categoryID { get; set; }
            public string quantity { get; set; }

        }
        public Order()
        {
            InitializeComponent();
        }

        class StudentDao
        {
            public Tuple<List<SmartPhone>, int> GetAll(
                int currentPage = 1, int rowsPerPage = 10,
                string keyword = "")
            {
                var origin = new ObservableCollection<SmartPhone>()
                {
                    new SmartPhone() { Name="Samsung Galaxy A14 6GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                    new SmartPhone() { Name="Vivo Y22s 8GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/285224/vivo-y22s-xanh-vang-1.jpg"},
                    new SmartPhone() { Name="Samsung Galaxy A14 6GB", Price="30.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                    new SmartPhone() {Name="iPhone 14 Pro 128GB", Price="45.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/247508/iphone14-pro-1.jpg"},
                    new SmartPhone() { Name="Samsung Galaxy A14 6GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                    new SmartPhone() { Name="Samsung Galaxy A14 6GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                    new SmartPhone() { Name="Samsung Galaxy A14 6GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                    new SmartPhone() { Name="Samsung Galaxy A14 6GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                    new SmartPhone() { Name="Samsung Galaxy A14 6GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                    new SmartPhone() { Name="Samsung Galaxy A14 6GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                    new SmartPhone() { Name="Samsung Galaxy A14 6GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                    new SmartPhone() { Name="Samsung Galaxy A14 6GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                    new SmartPhone() { Name="Samsung Galaxy A14 6GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                    new SmartPhone() {Name="Samsung Galaxy A14 6GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                    new SmartPhone() { Name="Samsung Galaxy A14 6GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                    new SmartPhone() {Name="Samsung Galaxy A14 6GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                    new SmartPhone() {Name="Samsung Galaxy A14 6GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                    new SmartPhone() { Name="Samsung Galaxy A14 6GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                    new SmartPhone() { Name="Samsung Galaxy A14 6GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                    new SmartPhone() { Name="Samsung Galaxy A14 6GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                    new SmartPhone() { Name="Samsung Galaxy A14 6GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                    new SmartPhone() { Name="Samsung Galaxy A14 6GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                    new SmartPhone() { Name="Samsung Galaxy A14 6GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                    new SmartPhone() { Name="Samsung Galaxy A14 6GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                    new SmartPhone() { Name="Samsung Galaxy A14 6GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                    new SmartPhone() { Name="Samsung Galaxy A14 6GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                    new SmartPhone() {Name="Samsung Galaxy A14 6GB", Price="35.000", imagePath="https://cdn.tgdd.vn/Products/Images/42/292770/samsung-galaxy-a14-black-1.jpg"},
                };
                var list = origin.Where(
                    item => item.Name.Contains(keyword)
                );

                var items = list.Skip((currentPage - 1) * rowsPerPage)
                    .Take(rowsPerPage);


                var result = new Tuple<List<SmartPhone>, int>(
                    items.ToList(), list.Count()
                );
                return result;
            }
        }

        List<SmartPhone> _students = null;

        int _currentPage = 1;
        int _rowsPerPage = 10;
        int _totalItems = 0;
        int _totalPages = 0;
        StudentDao _dao = new StudentDao();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _updateDataSource(1);
            _updatePagingInfo();
            

            currentPageComboBox.SelectedIndex = _currentPage - 1;
        }
   

        private void _updateDataSource(int page, string keyword = "")
        {
            _currentPage = page;
            (_students, _totalItems) = _dao.GetAll(
                _currentPage, _rowsPerPage, keyword);







            dataListView.ItemsSource = _students;
            infoTextBlock.Text =
                $"Đang hiển thị {_students.Count} / {_totalItems} điện thoại";
        }



        private void _updatePagingInfo()
        {
            _totalPages = _totalItems / _rowsPerPage +
                   (_totalItems % _rowsPerPage == 0 ? 0 : 1);

            // Cập nhật ComboBox
            var lines = new List<Tuple<int, int>>();
            for (int i = 1; i <= _totalPages; i++)
            {
                lines.Add(new Tuple<int, int>(i, _totalPages));
            }
            currentPageComboBox.ItemsSource = lines;


            currentPageComboBox.ItemsSource = lines;
            currentPageComboBox.SelectedIndex = _currentPage - 1;

            // Cập nhật nút Previous
            previousButton.IsEnabled = _currentPage == 1;

            // Cập nhật nút Next
            nextButton.IsEnabled = _currentPage < _totalPages;
        }

        private void currentPageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (currentPageComboBox.SelectedIndex >= 0)
            {
                _currentPage = currentPageComboBox.SelectedIndex + 1;

                _updateDataSource(_currentPage);
            }
        }

        /*      private void sortOptionRadioButton_Checked(object sender, RoutedEventArgs e)
              {
                  _updateDataSource(_currentPage);
              }*/



        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            string keyword = keywordTextBox.Text;

            _updateDataSource(1, keyword);
            _updatePagingInfo();

            currentPageComboBox.SelectedIndex = _currentPage - 1;
        }
        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
            {
                _updateDataSource(_currentPage - 1);
                currentPageComboBox.SelectedIndex = _currentPage - 1;
            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _updateDataSource(_currentPage + 1);
                currentPageComboBox.SelectedIndex = _currentPage - 1;
            }
        }

        private void previousButton_Click_1(object sender, RoutedEventArgs e)
        {



        }

        private void dataListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        
        }

      
    }
}
