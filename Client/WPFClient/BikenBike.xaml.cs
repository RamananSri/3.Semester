using System.Windows;
using WPFClient.Advertisements;
using WPFClient.BikeService;
using WPFClient.Search;
using WPFClient.Users;

namespace WPFClient
{
    public partial class BikenBike : Window
    {
        public static User CurrentUser { get; set; }


        public BikenBike()
        {
            InitializeComponent();
            this.Content = new StartPage(this);
        }
    }
}
