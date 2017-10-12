using System;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFClient.BikeService;
using WPFClient.Bookings;
using WPFClient.Search;

namespace WPFClient.Advertisements
{
    public partial class ViewAdPage : Page
    {
        private readonly BikeServiceClient _bikeService;
        private readonly Window _window;
        static Advertisement _ad;
        private bool _bookable;

        public ViewAdPage(Window window, int id, bool bookable)
        {
            InitializeComponent();
            _window = window;
            _bookable = bookable;
            _bikeService = new BikeServiceClient();
            Populate(id);
        }

        private void Populate(int id)
        {
            Advertisement ad = null;

            try
            {
                ad = _bikeService.FindAdById(id);
            }
            catch (FaultException e)
            {
                MessageBox.Show(e.Message);
            }

            if (ad != null)
            {
                lblTitle.Content = ad.Title;
                lblBrand.Content = ad.Bike.Brand.Name;
                lblDesc.Content = ad.Description;
                lblEDate.Content = ad.EndDate;
                lblFrame.Content = ad.Bike.FrameSize.Size;
                lblPrice.Content = ad.Price;
                lblSDate.Content = ad.StartDate;
                lblType.Content = ad.Bike.Type.TypeName;
                lblWheel.Content = ad.Bike.WheelSize.Size;
                lblYear.Content = ad.Bike.Year;
                _ad = ad;
            }

            if (BikenBike.CurrentUser == null)
            {
                dpEDate.Visibility = Visibility.Hidden;
                lblStart.Visibility = Visibility.Hidden;
                dpSDate.Visibility = Visibility.Hidden;
                lblEnd.Visibility = Visibility.Hidden;

                lblTotal.Visibility = Visibility.Hidden;
                lblTotalPriceInfo.Visibility = Visibility.Hidden;
                btnBook.Visibility = Visibility.Hidden;
            }

   
        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (_bookable)
            {
                _window.Content = new SearchPage(_window,true);
            }
            else
            {
                _window.Content = new ShowAds(_window);
            }  
        }

        private async void btnBook_Click(object sender, RoutedEventArgs e)
        {
            Booking b = null;

            if (dpSDate.SelectedDate != null && dpEDate.SelectedDate != null)
            {
                b = new Booking
                {
                    AdvertismentId = _ad.Id,
                    EndDate = Convert.ToDateTime(dpEDate.SelectedDate),
                    StartDate = Convert.ToDateTime(dpSDate.SelectedDate),
                    RentUserId = BikenBike.CurrentUser.Id,
                    TotalPrice = Convert.ToDouble(lblTotal.Content)
                };
            }
            Task bookingTask = CreateBookingLogic(b);
            await bookingTask;
        }

        private async Task CreateBookingLogic(Booking b)
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    lblLoading.Content = "LOADING..";
                });

                await _bikeService.CreateBookingAsync(b);

                Dispatcher.Invoke(() =>
                {
                    _window.Content = new MyBookingsPage(_window);
                });
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("forbindelse ikke fundet");
            }

            catch (FaultException e)
            {
                MessageBox.Show(e.Message);

                this.Dispatcher.Invoke(() =>
                {
                    _window.Content = new ViewAdPage(_window,_ad.Id,false);
                });
            }
        }

        private void DpEDate_OnCalendarClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                lblTotal.Content = _bikeService.CalcPrice((DateTime) dpSDate.SelectedDate, (DateTime) dpEDate.SelectedDate, (double)lblPrice.Content);
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}