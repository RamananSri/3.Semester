using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using WPFClient.BikeService;
using WPFClient.Search;

namespace WPFClient.Users
{
    public partial class LoginPage : Page
    {
        private BikeServiceClient BikeService;
        private Window window;
//        private Thread t;

        public LoginPage(Window window)
        {
            InitializeComponent();
            BikeService = new BikeServiceClient();
            this.window = window;
//            t = new Thread(new ParameterizedThreadStart(Login));
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

                        User user = BikeService.LoginUser(txtUsername.Text, txtPassword.Text);
            
                        if (user != null)
                        {
                            try
                            {
                                BikenBike.CurrentUser = user;
                                window.Content = new StartPage(window);
                            }
                            //catch hvis de skriver forkert brugernavn/kode
                            catch (Exception exception)
                            {
                                Console.WriteLine(exception);
                                throw;
                            }
                        }
                        else
                        {
                            lblError.Content = "der er sket en fejl";
                        }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            window.Content = new RegisterUserPage(window);
            
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            window.Content = new StartPage(window);
        }
    }
}
