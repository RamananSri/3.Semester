using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using ModelLayer;

namespace ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IBikeService
    {
        #region BikeService

        [OperationContract]
        void CreateBicycle(Bicycle b);

        [OperationContract]
        List<Bicycle> GetBikesByUser(int Id);

        [OperationContract]
        List<Brand> GetBrands();

        [OperationContract]
        List<Wheel> GetWheelSizes();

        [OperationContract]
        List<Frame> GetFrameSizes();

        [OperationContract]
        List<BicycleType> GetTypes();

        [OperationContract]
        Bicycle GetBikeByYear(string year);

        [OperationContract]
        void RemoveBicycle(int bicycleId);

        #endregion

        #region AdService

        [OperationContract]
        void CreateAd(string title, string description, double price, DateTime startDate, DateTime endDate, int? bikeId, int? userId);

        [OperationContract]
        void ModifyAd(int id, string title, string description, double price, DateTime startDate, DateTime endDate, int? bikeId, int? userId);

        [OperationContract]
        void RemoveAd(int id);

        [OperationContract]
        Advertisement GetAdByTitle(string title);

        [OperationContract]
        List<Advertisement> GetAdvertisementsByUser(int Id);

        [OperationContract]
        Advertisement FindAdById(int id);

        [OperationContract]
        List<Advertisement> GetAllAds();

        [OperationContract]
        List<Advertisement> GetAvailableAds(DateTime start, DateTime end);

        #endregion

        #region User Service

        [OperationContract]
        void CreateUser(string email, string pword, string name, string phone, string address, string zipcode, string age);

        [OperationContract]
        void ModifyUser(int id, string email, string name, string phone, string address, string zipcode, string age);

        [OperationContract]
        void RemoveUser(int id);

        [OperationContract]
        List<User> GetAllUsers();

        [OperationContract]
        User GetUser(int id);

        [OperationContract]
        User GetUserByEmail(string email);

        [OperationContract]
        User LoginUser(string email, string pword);

        #endregion

        #region BookingService

        [OperationContract]
        void CreateBooking(Booking b);

        [OperationContract]
        void RemoveBooking(int bookingId);

        [OperationContract]
        List<Booking> GetBookingsByUser(int userId);

        [OperationContract]
        Booking GetBookingByPrice(double price);

        [OperationContract]
        double CalcPrice(DateTime startDate, DateTime endDate, double price);

        #endregion
    }
}
