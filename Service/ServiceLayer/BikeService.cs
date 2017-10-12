using System;
using System.Collections.Generic;
using System.ServiceModel;
using BusinessLogicLayer;
using ModelLayer;

namespace ServiceLayer
{
    [ServiceBehavior(
        ConcurrencyMode = ConcurrencyMode.Multiple,
        InstanceContextMode = InstanceContextMode.PerSession)]

    public class BikeService : IBikeService
    {
        private AdvertisementCtrl ACtrl;
        private UserCtrl uCtrl;
        private BicycleCtrl BCtrl;
        private BookingCtrl bookingCtrl;

        public BikeService()
        {
            ACtrl = new AdvertisementCtrl();
            BCtrl = new BicycleCtrl();
            uCtrl = new UserCtrl();
            bookingCtrl = new BookingCtrl();
        }

        #region Bike Service

        public void CreateBicycle(Bicycle b)
        {
            BCtrl.CreateBicycle(b);
        }

        public List<Bicycle> GetBikesByUser(int Id)
        {
            try
            {
                return BCtrl.GetBikesByUser(Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Brand> GetBrands()
        {
            try
            {
                return BCtrl.GetBrands();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BicycleType> GetTypes()
        {
            try
            {
                return BCtrl.GetTypes();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Wheel> GetWheelSizes()
        {
            try
            {
                return BCtrl.GetWheelSizes();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Frame> GetFrameSizes()
        {
            try
            {
                return BCtrl.GetFrameSizes();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Bicycle GetBikeByYear(string year)
        {
            try
            {
                return BCtrl.GetBikeByYear(year);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveBicycle(int bicycleId)
        {
            BCtrl.RemoveBicycle(bicycleId);
        }

        #endregion

        #region Ad Service

        public void CreateAd(string title, string description, double price, DateTime startDate, DateTime endDate, int? bikeId, int? userId)
        {
            try
            {
                ACtrl.CreateAd(title, description, price, startDate, endDate, bikeId, userId);
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }

        public void ModifyAd(int id, string title, string description, double price, DateTime startDate, DateTime endDate, int? bikeId, int? userId)
        {
            // TODO: object + id
            ACtrl.ModifyAd(id, title, description, price, startDate, endDate, bikeId, userId);
        }

        public void RemoveAd(int id)
        {
            ACtrl.RemoveAd(id);
        }

        public Advertisement GetAdByTitle(string title)
        {
            try
            {
                return ACtrl.GetAdByTitle(title);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Advertisement> GetAdvertisementsByUser(int Id)
        {
            try
            {
                List<Advertisement> ad = ACtrl.GetAdvertisementsByUser(Id);
                return ad;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Advertisement FindAdById(int id)
        {
            try
            {
                return ACtrl.FindAdById(id);
            }

            catch (FaultException)
            {
                throw;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public List<Advertisement> GetAllAds()
        {
            try
            {
                return ACtrl.GetAllAds();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Advertisement> GetAvailableAds(DateTime start, DateTime end)
        {
            return ACtrl.GetAvailableAds(start, end);
        }

        #endregion

        #region User Service

        public void CreateUser(string email, string pword, string name, string phone, string address, string zipcode, string age)
        {
            uCtrl.AddUser(email, pword, name, phone, address, zipcode, age);
        }


        public void RemoveUser(int id)
        {
            uCtrl.RemoveUser(id);
        }

        public List<User> GetAllUsers()
        {
            try
            {
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User LoginUser(string email, string pword)
        {
            try
            {
                return uCtrl.LoginUser(email, pword);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User GetUser(int id)
        {
            try
            {
                return uCtrl.GetUser(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ModifyUser(int id, string email, string name, string phone, string address, string zipcode, string age)
        {
            uCtrl.ModifyUser(id, email, name, phone, address, zipcode, age);
        }

        public User GetUserByEmail(string email)
        {
            try
            {
                return uCtrl.GetUserByEmail(email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region BookingService

        public void CreateBooking(Booking b)
        {
            try
            {
                bookingCtrl.CreateBooking(b);
            }
            catch (FaultException) { throw; }
        }



        public void RemoveBooking(int bookingId)
        {
            bookingCtrl.RemoveBooking(bookingId);
        }

        public List<Booking> GetBookingsByUser(int userId)
        {
            try
            {
                return bookingCtrl.GetBookingsByUser(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Booking GetBookingByPrice(double price)
        {
            try
            {
                return bookingCtrl.GetBookingByPrice(price);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public double CalcPrice(DateTime startDate, DateTime endDate, double price)
        {
            try
            {
                return bookingCtrl.CalcPrice(startDate, endDate, price);
            }
            catch(FaultException)
            {
                throw;
            }
        }

        #endregion
    }
}
