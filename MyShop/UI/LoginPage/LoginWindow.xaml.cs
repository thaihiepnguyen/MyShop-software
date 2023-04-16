using MyShop.UI.SignupPage;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using MyShop.BUS;
using MyShop.DTO;
using MyShop.UI.MainPage.Pages;

namespace MyShop.UI.LoginPage
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        class Resoures
        {
            public string Logo { get; set; }
            public string MainBgPath { get; set; }
        }
        private void Button_Login(object sender, RoutedEventArgs e)
        {
            string inputUsername = txtUsername.Text;
            string inputPassword = txtPassword.Password;

            UserBUS productBUS = new UserBUS();
            UserDTO handleApiUser = productBUS.getOne(inputUsername, inputPassword);

            if (handleApiUser != null)
            {
                Button button = sender as Button;
                Window parentWindow = Window.GetWindow(button);
                parentWindow.Close();
            }
            else
            {
                txtFailLogin.Text = "Invalid username or password";
                Trace.WriteLine("invalid Username or password");
            }
        }

        private void Button_Signup(object sender, RoutedEventArgs e)
        {
            SignupWindow loginWindow = new SignupWindow();
            loginWindow.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new Resoures()
            {
                Logo = "Assets/Images/logo.png",
                MainBgPath = "Assets/Images/main-bg.png"
            };

        }
    }
}
