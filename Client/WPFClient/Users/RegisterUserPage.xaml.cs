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
using WPFClient.BikeService;
using WPFClient.Search;

namespace WPFClient.Users
{
    public partial class RegisterUserPage : Page
    {
        private BikeServiceClient BikeService;
        private Window window;

        public RegisterUserPage(Window window)
        {
            InitializeComponent();
            BikeService = new BikeServiceClient();
            this.window = window;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BikeService.Open();
                BikeService.CreateUser(txtEmail.Text, txtPassword.Text, txtName.Text, txtPhone.Text, txtAddress.Text, txtZipcode.Text, txtAge.Text);
                BikeService.Close();

                window.Content = new StartPage(window);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    } 
}
