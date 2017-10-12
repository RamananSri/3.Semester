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
using WPFClient.BikeService;
using Frame = WPFClient.BikeService.Frame;

namespace WPFClient.Bicycles
{
    /// <summary>
    /// Interaction logic for CreateBikePage.xaml
    /// </summary>
    public partial class CreateBikePage : Page
    {

        private BikeServiceClient BikeService;      // Service connection
        private List<Brand> Brands;                             // Liste af eksisterende brands
        private Window window;

        public CreateBikePage(Window window)
        {
            InitializeComponent();
            BikeService = new BikeServiceClient();
            this.window = window;
            PopulateComboBoxes();
        }

        // Udfylder standard dropdowns fra service
        private void PopulateComboBoxes()
        {
            Brands = BikeService.GetBrands();
            BrandComboBox.ItemsSource = Brands;

            List<Frame> Frames = BikeService.GetFrameSizes();
            FrameComboBox.ItemsSource = Frames;

            List<Wheel> Wheels = BikeService.GetWheelSizes();
            WheelComboBox.ItemsSource = Wheels;

            List<BicycleType> Types = BikeService.GetTypes();
            TypeComboBox.ItemsSource = Types;
        }

        private void CreateBtnClick(object sender, RoutedEventArgs e)
        {
            if (TypeComboBox.SelectedIndex != -1 &&
                BrandComboBox.SelectedIndex != -1 &&
                !String.IsNullOrWhiteSpace(YeartextBox.Text))
            {
                Brand brand = (Brand) BrandComboBox.SelectionBoxItem;

                BicycleType bType = (BicycleType) TypeComboBox.SelectionBoxItem;

                Wheel wheel = (Wheel) WheelComboBox.SelectionBoxItem;

                Frame frame = (Frame) FrameComboBox.SelectionBoxItem;

//                BikeService.CreateBicycle(YeartextBox.Text, brand.Id,
//                    bType.Id, wheel.Id, frame.Id, 1);

                Bicycle b = new Bicycle
                {
                    WheelSizeId = wheel.Id,
                    BrandId = brand.Id,
                    FrameSizeId = frame.Id,
                    TypeId = bType.Id,
                    Year = YeartextBox.Text,
                    UserId = BikenBike.CurrentUser.Id
                    
                };

                BikeService.CreateBicycle(b);

                window.Content = new BicyclePage(window);

            }
        }

        // Event - Cancel knap lukker vinduet
        private void CancelBtnClick(object sender, RoutedEventArgs e)
        {
            window.Content = new BicyclePage(window);
        }
    }
}
