using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using WPFClient.BikeService;
using WPFClient.Search;
using WPFClient.Users;

namespace WPFClient.Advertisements
{
    public partial class AdPage : Page
    {
        private readonly Window _window;
        private List<Advertisement> _ads;
        private readonly BikeServiceClient _service;

        public AdPage(Window window)
        {
            InitializeComponent();
            _service = new BikeServiceClient();
            this._window = window;
            PopulateList();
        }

        private void PopulateList()
        {
            _ads = _service.GetAdvertisementsByUser(BikenBike.CurrentUser.Id);

            if (_ads != null)
            {
                foreach (var ad in _ads)
                {
                    listView.Items.Add(ad);
                }
            }

        }

        private void CreateAdBtn_Click(object sender, RoutedEventArgs e)
        {
            _window.Content = new CreateAdPage(_window);
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            _window.Content = new StartPage(_window);
        }

        private void DeleteBtnClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                Advertisement ad = (Advertisement)btn.DataContext;
                _service.RemoveAd(ad.Id);
                _window.Content = new AdPage(_window);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
