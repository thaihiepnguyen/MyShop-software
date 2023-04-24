using MyShop.BUS;
using MyShop.DAO;
using MyShop.DTO;
using MyShop.UI.LoginPage;
using MyShop.UI.MainPage.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MD5CryptoServiceProvider = System.Security.Cryptography.MD5CryptoServiceProvider;

namespace MyShop.UI.SignupPage
{
    /// <summary>
    /// Interaction logic for SignupWindow.xaml
    /// </summary>
    public partial class SignupWindow : Window
    {
        public SignupWindow()
        {
            InitializeComponent();
        }
        class Resoures
        {
            public string Logo { get; set; }
            public string MainBgPath { get; set; }
        }


        private void Button_Signup_Click(object sender, RoutedEventArgs e)
        {
            string inputUsername = txtUsername.Text;
            string inputPassword = txtPassword.Password;
            string inputFullname = txtFullname.Text;
            string inputAdress = txtAdress.Text;
            string inputNumberphone = txtNumberphone.Text;

            if (string.IsNullOrEmpty(inputUsername) || string.IsNullOrEmpty(inputPassword) || string.IsNullOrEmpty(inputFullname) || string.IsNullOrEmpty(inputAdress) || string.IsNullOrEmpty(inputNumberphone))
                txtFailSignUp.Text = "Please enter all information";
            else
            {

                AesHelper aesHelper = new AesHelper();
                string encryptedText = aesHelper.Encrypt(inputPassword);
                string decryptedText = aesHelper.Decrypt(encryptedText);


                UserDTO userDTO = new UserDTO
                {
                    Username = inputUsername,
                    Password = encryptedText,
                    Fullname = inputFullname,
                    Gender = "Male",
                    Address = inputAdress,
                    Tel = inputNumberphone,
                    AvatarPath = "path/to/avatar.jpg",
                    IsHide = 0,
                    RoleID = 1
                };


                UserBUS userBUS = new UserBUS();

                bool signupSucess = userBUS.createUser(userDTO);
                if (signupSucess)
                {
                    Trace.WriteLine("Sucess");
                    Button button = sender as Button;
                    Window parentWindow = Window.GetWindow(button);
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.Show();
                    parentWindow.Close();
                }
                else
                {
                    Trace.WriteLine("Failure");
                    txtFailSignUp.Text = "Failed to create new account";
                }
            }

        }

        private void txtUsername_Copy_dsds_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            new DatabaseUtilitites(
                 "sqlexpress",
                 "MyShopDB_7",
                 "admin",
                 "admin"
           );
            this.DataContext = new Resoures()
            {
                Logo = "Assets/Images/logo.png",
                MainBgPath = "Assets/Images/main-bg.png"
            };
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

    }
}
