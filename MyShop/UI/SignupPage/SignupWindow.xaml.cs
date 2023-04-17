using MyShop.BUS;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string inputUsername = txtUsername.Text;
            string inputPassword = txtPassword.Password;
            string inputFullname = txtFullname.Text;
            string inputAdress = txtAdress.Text;
            string inputNumberphone = txtNumberphone.Text;

            // 
            UserDTO userDTO = new UserDTO
            {
                Username = inputUsername,
                Password = inputPassword,
                Fullname = inputFullname,
                Gender = "Male",
                Address = inputAdress,
                Tel = inputNumberphone,
                AvatarPath = "path/to/avatar.jpg",
                IsHide =0,
                RoleID = 1
            }; 


            UserBUS userBUS = new UserBUS();

            bool signupSucess = userBUS.createUser(userDTO);
            if(signupSucess)
            {
                Trace.WriteLine("Sucess");
                Button button = sender as Button;
                Window parentWindow = Window.GetWindow(button);
                parentWindow.Close();          
            }
            else
            {
                Trace.WriteLine("Failure");
            }
        }

        private void txtUsername_Copy_dsds_TextChanged(object sender, TextChangedEventArgs e)
        {

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
