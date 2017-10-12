using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using WPFClient.Advertisements;
using WPFClient.Bicycles;
using WPFClient.Bookings;
using WPFClient.Users;
using FlowDirection = System.Windows.FlowDirection;
using MenuItem = System.Windows.Controls.MenuItem;

namespace WPFClient.Search
{
    public partial class StartPage : Page
    {
        private readonly Window _window;

        public StartPage(Window window)
        {
            InitializeComponent();
            _window = window;
            LoginCheck();
        }

        private void LoginCheck()
        {
            if (BikenBike.CurrentUser != null)
            {
                MenuItem user = new MenuItem();
                user.Header = BikenBike.CurrentUser.Email;
                user.Height = 40;
                user.FontSize = 20;
                user.FlowDirection = FlowDirection.LeftToRight;
                menuDock.Items.Add(user);

                MenuItem ads = new MenuItem();
                ads.Header = "My ads";
                ads.Click += MyAdsBtnClicked; 
                MenuItem bikes = new MenuItem();
                bikes.Header = "My bikes";
                bikes.Click += MyBikesBtnClicked;
                MenuItem bookings = new MenuItem();
                bookings.Header = "My bookings";
                bookings.Click += MyBookingsBtnClicked;
                MenuItem profile = new MenuItem();
                profile.Header = "Profile";
                profile.Click += ProfileBtnClicked;
                MenuItem logout = new MenuItem();
                logout.Header = "Logout";
                logout.Click += LogoutBtnClicked;

                user.Items.Add(profile);
                user.Items.Add(bookings);
                user.Items.Add(ads);
                user.Items.Add(bikes);
                user.Items.Add(logout);
            }

            if (BikenBike.CurrentUser == null)
            {
                MenuItem register = new MenuItem();
                register.Header = "Register";
                register.Height = 40;
                register.FontSize = 20;
                register.Click += RegisterBtnClicked;

                MenuItem login = new MenuItem();
                login.Header = "Login";
                login.Height = 40;
                login.FontSize = 20;
                login.Click += LogimBtnClick;

                menuDock.Items.Add(login);
                menuDock.Items.Add(register);
            }
        }

        private void LogoutBtnClicked(object sender, RoutedEventArgs e)
        {
            BikenBike.CurrentUser = null;
            _window.Content = new StartPage(_window);
        }

        private void ProfileBtnClicked(object sender, RoutedEventArgs e)
        {
            _window.Content = new EditUserPage(_window);
        }

        private void MyBookingsBtnClicked(object sender, RoutedEventArgs e)
        {
            _window.Content = new MyBookingsPage(_window);
        }

        private void MyBikesBtnClicked(object sender, RoutedEventArgs e)
        {
            _window.Content = new BicyclePage(_window);
        }

        private void MyAdsBtnClicked(object sender, RoutedEventArgs e)
        {
            _window.Content = new AdPage(_window);
        }

        private void LogimBtnClick(object sender, RoutedEventArgs e)
        {
            _window.Content = new LoginPage(_window);
        }

        private void SearchBtnClicked(object sender, RoutedEventArgs e)
        {
            _window.Content = new SearchPage(_window,true);
        }

        private void RegisterBtnClicked(object sender, RoutedEventArgs e)
        {
            _window.Content = new RegisterUserPage(_window);
        }
    }
}
