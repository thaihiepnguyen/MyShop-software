using MyShop.UI.SignupPage;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using MyShop.BUS;
using MyShop.DTO;
using MyShop.UI.MainPage.Pages;
using MyShop.DAO;
using MyShop.UI.MainPage;
using DocumentFormat.OpenXml.CustomProperties;
using System.Linq;

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
                bool remember = RememberMeCheckBox.IsChecked == true;
                Properties.Settings.Default.IdUser = handleApiUser.UserID;
                Properties.Settings.Default.Save();
                if (remember && inputUsername != null)
                {
                    Properties.Settings.Default.UsernameRemember = true;
                    Properties.Settings.Default.Save();
                }
                Trace.WriteLine("Sucess");
                Button button = sender as Button;
                Window parentWindow = Window.GetWindow(button);
                MainWindow mainPage = new MainWindow();
                mainPage.Show();
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
            SignupWindow signUpWindow = new SignupWindow();
            signUpWindow.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            new DatabaseUtilitites(
                  "sqlexpress",
                  "MyShopDB_9",
                  "admin",
                  "admin"
            );

            if (Properties.Settings.Default.UsernameRemember)
            {

                MainWindow mainPage = new MainWindow();
                mainPage.Show();
                this.Close();
            }
            this.DataContext = new Resoures()
            {
                Logo = "Assets/Images/logo.png",
                MainBgPath = "Assets/Images/main-bg.png"
            };

        }
    }
}
