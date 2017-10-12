using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using WPFClient.Advertisements;
using WPFClient.BikeService;
using WPFClient.Bookings;

namespace WPFClient.Search
{
    public partial class SearchPage : Page
    {
        readonly BikeServiceClient _bikeService = new BikeServiceClient();
        private readonly Window _window;
        private bool _bookable;

        public SearchPage(Window window, bool bookable)
        {
            _window = window;
            _bookable = bookable;
            InitializeComponent();
            PopulateAdslistView();
        }


        private void PopulateAdslistView()
        {
            try
            {
                var ads = _bikeService.GetAllAds();

                if (ads != null)
                {
                    foreach (var ad in ads)
                    {
                        AdslistView.Items.Add(ad);
                    }
                }
            }
            catch (FaultException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void btnViewAd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button) sender;
                Advertisement ad = (Advertisement) btn.DataContext;
                _window.Content = new ViewAdPage(_window, ad.Id,true);
            }
            catch (FaultException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (_bookable)
            {
                _window.Content = new StartPage(_window);
            }
            else
            {
                _window.Content = new MyBookingsPage(_window);
            }
        }
    }
}
