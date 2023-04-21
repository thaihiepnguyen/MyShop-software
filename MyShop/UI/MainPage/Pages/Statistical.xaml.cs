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
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;
            int userId = 2;
            SaleBUS saleBUS = new SaleBUS();
            decimal salesYear = saleBUS.getSaleByYear(userId, currentYear);
            decimal salesMonth = saleBUS.getSaleByMonth(userId, currentYear);
            decimal salesDay = saleBUS.getSaleByDate(userId);
            this.DataContext = new DateSale() { year=salesYear, month=salesMonth, day=salesDay };

        }
    }
}
