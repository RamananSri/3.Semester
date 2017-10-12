using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFClient.BikeService;
using WPFClient.Search;

namespace WPFClient.Users
{
    /// <summary>
    /// Interaction logic for EditUserPage.xaml
    /// </summary>
    public partial class EditUserPage : Page
    {
        BikeServiceClient BikeService = new BikeServiceClient();
        private Window window;

        public EditUserPage(Window window)
        {
            InitializeComponent();
            this.window = window;

            try
            {
                if (BikenBike.CurrentUser != null)
                {
                    txtEmail.Text = BikenBike.CurrentUser.Email;
                    txtName.Text = BikenBike.CurrentUser.Name;
                    txtPhone.Text = BikenBike.CurrentUser.Phone;
                    txtAddress.Text = BikenBike.CurrentUser.Address;
                    txtZipcode.Text = BikenBike.CurrentUser.Zipcode;
                    txtAge.Text = BikenBike.CurrentUser.Age;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BikenBike.CurrentUser != null)
                {
                    BikeService.Open();
                    BikeService.ModifyUser(Convert.ToInt16(BikenBike.CurrentUser.Id), txtEmail.Text, txtName.Text, txtPhone.Text, txtAddress.Text, txtZipcode.Text, txtAge.Text);
                    BikeService.Close();
                    window.Content = new EditUserPage(window);

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (chkDelete.IsChecked.Value == true)
            {
                BikeService.Open();
                BikeService.RemoveUser(Convert.ToInt16(BikenBike.CurrentUser.Id));
                BikeService.Close();
                BikenBike bnb = new BikenBike();
                bnb.Show();
                window.Close();

            }
            else
            {
                MessageBox.Show("Check the checkbox before delete", "Bikenbike");
            }
        }

        private void BackBtn_OnClick(object sender, RoutedEventArgs e)
        {
            window.Content = new StartPage(window);
        }
    }
}
