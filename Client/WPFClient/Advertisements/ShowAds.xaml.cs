using System;
using System.Windows;
using System.Windows.Controls;
using WPFClient.BikeService;
using WPFClient.Bookings;

namespace WPFClient.Advertisements
{

    public partial class ShowAds : Page
    {
        BikeServiceClient BikeService = new BikeServiceClient();
        private Window window;

        public ShowAds(Window window)
        {
            try
            {
                InitializeComponent();
                this.window = window;
                BikeService.Open();
                PopulateAdslistView();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void PopulateAdslistView()
        {
            try
            {
                var ads = BikeService.GetAllAds();

                if (ads != null)
                {
                    foreach (var ad in ads)
                    {
                        AdslistView.Items.Add(ad);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void btnViewAd_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Button btn = (Button)sender;
                Advertisement ad = (Advertisement)btn.DataContext;
                BikeService.FindAdById(ad.Id);
                window.Content = new ViewAdPage(window, ad.Id,false);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            window.Content = new MyBookingsPage(window);
        }
    }
}
