using System;
using System.Windows;
using System.Windows.Controls;
using WPFClient.BikeService;
using WPFClient.Search;
using WPFClient.Users;

namespace WPFClient.Bicycles
{
    public partial class BicyclePage : Page
    {
        private readonly BikeServiceClient service;
        private readonly Window window;

        public BicyclePage(Window window)
        {
            InitializeComponent();
            service = new BikeServiceClient();
            this.window = window;
            PopulateBikeview();
        }

        private void PopulateBikeview()
        {
            var bicycles = service.GetBikesByUser(BikenBike.CurrentUser.Id);

            if (bicycles != null)
            {
                foreach (var bike in bicycles)
                {
                    BikeListView.Items.Add(bike);
                }
            }      
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            window.Content = new StartPage(window);
        }

        private void DeleteBtnClicked(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Bicycle b = (Bicycle)btn.DataContext;
            service.RemoveBicycle(b.ID);
            window.Content = new BicyclePage(window);
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            window.Content = new CreateBikePage(window);
        }
    }
}