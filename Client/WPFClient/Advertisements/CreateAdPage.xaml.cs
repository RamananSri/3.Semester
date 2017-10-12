using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using WPFClient.BikeService;
using WPFClient.Search;

namespace WPFClient.Advertisements
{

    public partial class CreateAdPage : Page
    {
        private readonly BikeServiceClient _bikeService;    // Service reference
        private readonly Window _window;                    // Hovedvinduet

        public CreateAdPage(Window window)
        {
            InitializeComponent();
            _bikeService = new BikeServiceClient();
            _window = window;
            BikeComboBox.ItemsSource = _bikeService.GetBikesByUser(BikenBike.CurrentUser.Id);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            _window.Content = new AdPage(_window);
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _bikeService.CreateAd(
                    txtTitle.Text, txtDesc.Text, 
                    Convert.ToDouble(txtPrice.Text), 
                    Convert.ToDateTime(dpSDate.SelectedDate), 
                    Convert.ToDateTime(dpEDate.SelectedDate), 
                    Convert.ToInt32(BikeComboBox.SelectedValue), 
                    BikenBike.CurrentUser.Id);

                _window.Content = new StartPage(_window);
            }
            catch (FaultException exception)
            {
                MessageBox.Show("her er fejlen: " + exception.Message);
            }
        }
    }
}
