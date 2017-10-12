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
using WPFClient.Advertisements;
using WPFClient.BikeService;
using WPFClient.Search;

namespace WPFClient.Bookings
{
    /// <summary>
    /// Interaction logic for MyBookingsPage.xaml
    /// </summary>
    public partial class MyBookingsPage : Page
    {
        private Window window;
        private BikeServiceClient Service;

        public MyBookingsPage(Window window)
        {
            InitializeComponent();
            this.window = window;
            Service = new BikeServiceClient();
            populateList();
        }

        private void populateList()
        {
            List<Booking> bookings = Service.GetBookingsByUser(BikenBike.CurrentUser.Id);
            listView.ItemsSource = bookings;
        }


        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            window.Content = new ShowAds(window);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            window.Content = new StartPage(window);
        }

        private void DeleteBtnClicked(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Booking b = (Booking)btn.DataContext;
            Service.RemoveBooking(b.Id);
            window.Content = new MyBookingsPage(window);
        }
    }
}
