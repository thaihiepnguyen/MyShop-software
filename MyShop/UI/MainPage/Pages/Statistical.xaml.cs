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
using LiveCharts;
using LiveCharts.Wpf;

namespace MyShop.UI.MainPage.Pages
{
    /// <summary>
    /// Interaction logic for Statistical.xaml
    /// </summary>
    public partial class Statistical : Page
    {
        class DateSale
        {
            public decimal year { get; set; }
            public decimal month { get; set; }
            public decimal day { get; set; }

        }
        public Statistical()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            chart.Series.Add(new LineSeries()
            {
                Title = "Doanh thu theo tháng",
                Values = new ChartValues<double>() { 3, 5, 9000, 4 }
            });

            chart.Series.Add(new ColumnSeries()
            {
                StrokeThickness = 1,
                Title = "Các mặt hàng bán chạy",
                Values = new ChartValues<double>() { 5, 6, 1100, 7 }
            });

            chart.AxisX.Add(
                new Axis()
                {
                    Title = "Thang",
                    Labels = new string[] { "Mar", "Apr", "May", "Jun" }
                });
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DateCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void NextProductReport_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
