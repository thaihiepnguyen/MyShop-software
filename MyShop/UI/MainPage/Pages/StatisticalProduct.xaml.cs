using LiveCharts.Wpf;
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
using LiveCharts;
using DocumentFormat.OpenXml.Bibliography;


namespace MyShop.UI.MainPage.Pages
{
    /// <summary>
    /// Interaction logic for StatisticProduct.xaml
    /// </summary>
    public partial class StatisticalProduct : Page
    {
        private ReportBUS _reportBUS;
        private int _currentYear;
        private ProductBUS _productBUS;
        private ProductDTO _currentProduct;
        private Frame _pageNavigation;
        public StatisticalProduct(Frame pageNavigation)
        {
            _reportBUS = new ReportBUS();
            _productBUS = new ProductBUS();
            _pageNavigation = pageNavigation;
            InitializeComponent();
        }

        private void displayYearMode(ProductDTO product)
        {
            var quantities = _reportBUS.groupQuantityOfProductByYear(product);
            var valuesColChart = new ChartValues<int>();

            foreach (var item in quantities)
            {
                valuesColChart.Add(item);
            }

            chart.Series = new SeriesCollection();
            chart.AxisX = new AxesCollection();

            chart.Series.Add(new ColumnSeries()
            {
                Fill = Brushes.Chocolate,
                Title = "Số lượng đã bán theo năm",
                Values = valuesColChart
            });

            chart.AxisX.Add(
                new Axis()
                {
                    Foreground = Brushes.Black,
                    Title = "Year",
                    Labels = new string[] {
                        "Năm 2021",
                        "Năm 2022",
                        "Năm 2023",
                    }
                });
            Title.Text = "Đang hiển thị chế độ xem theo năm";
        }

        private void displayMonthMode(ProductDTO product, int year)
        {
            var quantities = _reportBUS.groupQuantityOfProductByMonth(product, year);

            var valuesColChart = new ChartValues<double>();

            foreach (var item in quantities)
            {
                valuesColChart.Add((double)item);
            }

            chart.Series = new SeriesCollection();
            chart.AxisX = new AxesCollection();

            chart.Series.Add(new ColumnSeries()
            {
                Fill = Brushes.Chocolate,
                Title = "Số lượng đã bán theo tháng",
                Values = valuesColChart
            });

            chart.AxisX.Add(
                new Axis()
                {
                    Foreground = Brushes.Black,
                    Title = "Month",
                    Labels = new string[] {
                        "Tháng 1",
                        "Tháng 2",
                        "Tháng 3",
                        "Tháng 4",
                        "Tháng 5",
                        "Tháng 6",
                        "Tháng 7",
                        "Tháng 8",
                        "Tháng 9",
                        "Tháng 10",
                        "Tháng 11",
                        "Tháng 12",
                    }
                });
            Title.Text = "Đang hiển thị chế độ xem theo tháng";
            MonthCombobox.IsEnabled = true;
            MonthCombobox.SelectedIndex = 0;
        }

        private void displayWeekMode(ProductDTO product, int month, int year)
        {
            var quantities = _reportBUS.groupQuantityOfProductByWeek(product, year, month);
            var valuesColChart = new ChartValues<double>();

            foreach (var item in quantities)
            {
                valuesColChart.Add((double)item);
            }

            chart.Series = new SeriesCollection();
            chart.AxisX = new AxesCollection();

            chart.Series.Add(new ColumnSeries()
            {
                Fill = Brushes.Chocolate,
                Title = "Số lượng đã bán theo tuần",
                Values = valuesColChart
            });

            chart.AxisX.Add(
                new Axis()
                {
                    Foreground = Brushes.Black,
                    Title = "Week",
                    Labels = new string[] {
                        "Tuần 1",
                        "Tuần 2",
                        "Tuần 3",
                        "Tuần 4",
                        "Tuần 5",
                    }
                });
            Title.Text = "Đang hiển thị chế độ xem theo tuần";
        }

        private void displayDateMode(ProductDTO product, DateTime startDate, DateTime endDate)
        {
            var pricesByDate = _reportBUS.groupQuantityOfProductByDate(product, startDate, endDate);

            var valuesColChart = new ChartValues<double>();

            foreach (var item in pricesByDate)
            {
                valuesColChart.Add((double)item);
            }

            chart.Series = new SeriesCollection();
            chart.AxisX = new AxesCollection();

            chart.Series.Add(new ColumnSeries()
            {
                Fill = Brushes.Chocolate,
                Title = "Số lượng đã bán theo ngày",
                Values = valuesColChart
            });

            chart.AxisX.Add(
                new Axis()
                {
                    Foreground = Brushes.Black,
                    Title = "Date",
                    Labels = _reportBUS.EachDayConverter(startDate, endDate)
                });
            Title.Text = "Đang hiển thị chế độ xem theo ngày";
        }

        private void YearCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = YearCombobox.SelectedIndex;
            if (i == -1)
            {
                return;
            }
            else
            {
                if (i == 1)
                {
                    displayMonthMode(_currentProduct, 2021);
                    _currentYear = 2021;
                }
                if (i == 2)
                {
                    displayMonthMode(_currentProduct, 2022);
                    _currentYear = 2022;
                }
                if (i == 3)
                {
                    displayMonthMode(_currentProduct, 2023);
                    _currentYear = 2023;
                }
            }
        }

        private void MonthCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = MonthCombobox.SelectedIndex;
            if (i == -1 || i == 0)
            {
                return;
            }
            else
            {
                displayWeekMode(_currentProduct, i, _currentYear);
            }
        }

        private void ProductsCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MonthCombobox.SelectedIndex = 0;
            YearCombobox.SelectedIndex = 0;
            StartDate.SelectedDate = null;
            EndDate.SelectedDate = null;
            MonthCombobox.IsEnabled = false;
            _currentProduct = (ProductDTO)ProductsCombobox.SelectedValue;
            displayYearMode(_currentProduct);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<ProductDTO> products = _productBUS.getAll();

            ProductsCombobox.ItemsSource = products;
            ProductsCombobox.SelectedIndex = 0;

            chart.AxisY.Add(new Axis
            {
                Foreground = Brushes.Black,
                Title = "Số lượng sản phẩm",
                MinValue = 0
            });
            Title.Text = "Đang hiển thị chế độ xem theo năm";
            _currentProduct = (ProductDTO)ProductsCombobox.SelectedValue;
            displayYearMode(_currentProduct);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _pageNavigation.NavigationService.GoBack();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var startDate = StartDate.SelectedDate;
            var endDate = EndDate.SelectedDate;

            displayDateMode( _currentProduct,(DateTime)startDate, (DateTime)endDate);
        }
    }
}
