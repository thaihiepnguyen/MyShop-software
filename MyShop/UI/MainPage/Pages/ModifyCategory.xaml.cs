using MyShop.BUS;
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
    /// Interaction logic for ModifyCategory.xaml
    /// </summary>
    public partial class ModifyCategory : Page
    {
        private CategoryBUS _categoryBUS;
        public ModifyCategory()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _categoryBUS= new CategoryBUS();

            var categories = _categoryBUS.getAll();

            categoriesListView.ItemsSource = categories;
        }

        private void SaveCategory_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
