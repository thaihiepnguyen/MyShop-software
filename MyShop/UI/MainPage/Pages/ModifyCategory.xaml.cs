using MyShop.BUS;
using MyShop.DAO;
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
    /// Interaction logic for ModifyCategory.xaml
    /// </summary>
    public partial class ModifyCategory : Page
    {
        private CategoryBUS _categoryBUS;
        private ObservableCollection<CategoryDTO> _categories;
        List<Icon> _icons = new List<Icon>() {
                new Icon("Android"),
                new Icon("Apple"),
                new Icon("Windows"),
                new Icon("MobilePhone"),
            };
        public ModifyCategory()
        {
            InitializeComponent();
        }

        public class Icon
        {
            public string CatIcon { get; set; }

            public Icon(string catIcon)
            {
                CatIcon = catIcon;
            }
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _categoryBUS= new CategoryBUS();

            _categories = _categoryBUS.getAll();


            categoriesListView.ItemsSource = _categories;
            CategoryCombobox.ItemsSource = _icons;

            CategoryCombobox.SelectedIndex = 0;
        }

        private void SaveCategory_Click(object sender, RoutedEventArgs e)
        {
            var category = new CategoryDTO();

            category.CatName = NameTermTextBox.Text;
            category.CatDescription = DesTermTextBox.Text;
            category.CatIcon = _icons[CategoryCombobox.SelectedIndex].CatIcon;
            

            int id = _categoryBUS.addCategory(category);

            category.CatID = id;
            _categories.Add(category);

            MessageBox.Show("Thể loại đã thêm thành công", "Thông báo", MessageBoxButton.OK);
        }
    }
}
