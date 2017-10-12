using System;
using System.Diagnostics;
using System.Linq;
using DataAccessLayer;
using DataAccessLayer.Migrations;
using DataAccessLayer.Modules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLayer;
using ServiceLayer;
using UnitTest.testServiceRef;

namespace UnitTest
{
    [TestClass]
    public class IntegrationTests
    {
        private BikeServiceClient client = new BikeServiceClient();
        private BookingDB bookingDB;
        private AdvertisementDB advertisementDB;
        private UserDB userDB;
        private BicycleDB bicycleDB;

        private Brand b1, b2, b3;
        private BicycleType bt1, bt2;
        private Frame f1, f2, f3, f4;
        private Wheel w1, w2, w3, w4;
        private User u1, u2;
        private Bicycle bike1, bike2, bike3, bike4, bike5, bike6;
        private Advertisement a1, a2;
        private CryptoModule crypto;

        [TestInitialize] 
        public void setup()
        {
            bookingDB = new BookingDB();
            advertisementDB = new AdvertisementDB();
            userDB = new UserDB();
            bicycleDB = new BicycleDB();
            crypto = new CryptoModule();

            // Seed Brands
            b1 = new Brand { Name = "Croissant" };
            b2 = new Brand { Name = "Canondale" };
            b3 = new Brand { Name = "Trek" };

            // Seed Types
            bt1 = new BicycleType { TypeName = "Mountainbike" };
            bt2 = new BicycleType { TypeName = "Racer" };

            // Seed FrameSizes
            f1 = new Frame { Size = 30 };
            f2 = new Frame { Size = 32 };
            f3 = new Frame { Size = 34 };
            f4 = new Frame { Size = 36 };

            // Seed WheelSizes
            w1 = new Wheel { Size = 20 };
            w2 = new Wheel { Size = 22 };
            w3 = new Wheel { Size = 24 };
            w4 = new Wheel { Size = 26 };

            // Seed Users

            u1 = new User
            {
                Address = "test",
                Age = "1",
                Salt = crypto.GenerateSaltString(),
                Email = "test",
                Name = "test",
                PWord = "1",
                Phone = "1234",
                Zipcode = "9200"
            };

            u2 = new User
            {
                Address = "Hobrovej",
                Age = "30",
                Salt = crypto.GenerateSaltString(),
                Email = "john@gmail.com",
                Name = "John",
                PWord = "123",
                Phone = "87654321",
                Zipcode = "9000"
            };

            // Seed bicycles

            bike1 = new Bicycle
            {
                Brand = b1,
                FrameSize = f1,
                Type = bt1,
                User = u1,
                WheelSize = w1,
                Year = "2017"
            };

            bike2 = new Bicycle
            {
                Brand = b2,
                FrameSize = f2,
                Type = bt2,
                User = u1,
                WheelSize = w2,
                Year = "2016"
            };

            bike3 = new Bicycle
            {
                Brand = b3,
                FrameSize = f3,
                Type = bt1,
                User = u1,
                WheelSize = w3,
                Year = "2015"
            };

            bike4 = new Bicycle
            {
                Brand = b3,
                FrameSize = f1,
                Type = bt1,
                User = u2,
                WheelSize = w1,
                Year = "2012"
            };

            bike5 = new Bicycle
            {
                Brand = b3,
                FrameSize = f3,
                Type = bt1,
                User = u2,
                WheelSize = w3,
                Year = "2011"
            };

            bike6 = new Bicycle
            {
                Brand = b3,
                FrameSize = f4,
                Type = bt1,
                User = u2,
                WheelSize = w4,
                Year = "2010"
            };

            // Seed advertisements

            a1 = new Advertisement
            {
                Title = "lorem 2",
                Description = "K0WJZFWWZW VLL4262TZI 81CS84SUUR OCWPGNS3X2 66WJ5APZLR BDCHCU3WEC",
                Price = 18.90,
                StartDate = DateTime.ParseExact("01/05/2017", "dd/MM/yyyy", null),
                EndDate = DateTime.ParseExact("01/07/2017", "dd/MM/yyyy", null),
                User = u2,
                Bike = bike4
            };

            a2 = new Advertisement
            {
                Title = "lorem 1",
                Description = "K0WJZFWWZW VLL4262TZI 81CS84SUUR OCWPGNS3X2 66WJ5APZLR BDCHCU3WEC",
                Price = 18.90,
                StartDate = DateTime.ParseExact("01/05/2017", "dd/MM/yyyy", null),
                EndDate = DateTime.ParseExact("01/07/2017", "dd/MM/yyyy", null),
                User = u1,
                Bike = bike2
            };
        }

        [TestCleanup]
        public void cleanup()
        {
        }

        [TestMethod]
        public void TestServiceConnection()
        {
            client.Open();
        }

        #region User test
        [TestMethod]
        public void TestRegisterUser()
        {
            client.CreateUser("daniel@daniel.dk", "test1234", "Daniel", "45454545", "Danielvej", "9000", "24");
            var actual = userDB.GetAllUser().Last();
            userDB.RemoveUser(actual.Id);
            Assert.AreEqual("daniel@daniel.dk", actual.Email);
        }

        [TestMethod]
        public void TestLogin()
        {

            User u1 = new User
            {
                Address = "test",
                Age = "1",
                Salt = crypto.GenerateSaltString(),
                Email = "test",
                Name = "test",
                PWord = "1",
                Phone = "1234",
                Zipcode = "9200"
            };
            userDB.AddUser(u1);

            User expected = client.LoginUser(u1.Email, u1.PWord);

            userDB.RemoveUser(expected.Id);

            Assert.AreEqual(expected.Email, u1.Email);
            Assert.AreEqual(expected.PWord, u1.PWord);
        }


        [TestMethod]
        public void TestModifyUser()
        {
            User u1 = new User
            {
                Address = "test",
                Age = "1",
                Salt = crypto.GenerateSaltString(),
                Email = "test",
                Name = "test",
                PWord = "1",
                Phone = "1234",
                Zipcode = "9200"
            };
            userDB.AddUser(u1);

            User actual = userDB.GetAllUser().Last();

            client.ModifyUser(actual.Id, "daniel@daniel.dk", "test", "1234", "test", "9200","1");

            User expected = userDB.GetAllUser().Last();

            userDB.RemoveUser(expected.Id);

            Assert.AreNotEqual(actual.Email, expected.Email);
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            User u1 = new User
            {
                Address = "test",
                Age = "1",
                Salt = crypto.GenerateSaltString(),
                Email = "test",
                Name = "test",
                PWord = "1",
                Phone = "1234",
                Zipcode = "9200"
            };
            userDB.AddUser(u1);

            User actual = userDB.GetAllUser().Last();

            client.RemoveUser(actual.Id);

            User expected = userDB.GetAllUser().Last();

            Assert.AreNotEqual(actual.Id,expected.Id);
        }


        #endregion

        #region Bike test

        [TestMethod]
        public void TestAddBicycle()
        {

            Bicycle bike = new Bicycle
            {
                Year = "2015",
                BrandId = bicycleDB.GetBrands().Last().Id,
                TypeId = bicycleDB.GetTypes().Last().Id,
                WheelSizeId = bicycleDB.GetWheelSizes().Last().Id,
                FrameSizeId = bicycleDB.GetFrameSizes().Last().Id,
                UserId = userDB.GetAllUser().Last().Id
            };

            bicycleDB.CreateBicyle(bike);

            client.CreateBicycle(bike);

            var actual = bicycleDB.GetAllBicycles().Last();

            Assert.AreEqual(bike.Year, actual.Year);
            Assert.AreEqual(bike.BrandId, actual.BrandId);
            Assert.AreEqual(bike.TypeId, actual.TypeId);
            Assert.AreEqual(bike.WheelSizeId, actual.WheelSizeId);
            Assert.AreEqual(bike.FrameSizeId, actual.FrameSizeId);
            Assert.AreEqual(bike.UserId, actual.UserId);
        }
        #endregion

        #region Ad test

        [TestMethod]
        public void TestAddAds()
        { 
            Advertisement ad = new Advertisement
            {
                Title = "A",
                Description = "A",
                Price = 50,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7),
                BikeId = bicycleDB.GetAllBicycles().Last().ID,
                UserID = userDB.GetAllUser().Last().Id,
            };
            client.CreateAd(ad.Title, ad.Description, ad.Price, ad.StartDate, ad.EndDate, ad.BikeId, ad.UserID);

            Advertisement actual = advertisementDB.GetAllAds().Last();

            advertisementDB.RemoveAd(actual.Id);

            Assert.AreEqual(ad.Title, actual.Title);
            Assert.AreEqual(ad.Description, actual.Description);
            Assert.AreEqual(ad.Price, actual.Price);
            Assert.AreEqual(ad.StartDate.ToString(), actual.StartDate.ToString());
            Assert.AreEqual(ad.EndDate.ToString(), actual.EndDate.ToString());
            Assert.AreEqual(ad.BikeId, actual.BikeId);
            Assert.AreEqual(ad.UserID, actual.UserID);
        }

        [TestMethod]
        public void TestModifyAds()
        {
            Advertisement ad = new Advertisement
            {
                Title = "A",
                Description = "A",
                Price = 50,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7),
                BikeId = bicycleDB.GetAllBicycles().Last().ID,
                UserID = userDB.GetAllUser().Last().Id,
            };
            advertisementDB.AddAd(ad);

            Advertisement a = advertisementDB.GetAllAds().Last();

            client.ModifyAd(a.Id,"B","B",a.Price,a.StartDate,a.EndDate,a.BikeId,a.UserID);

            Advertisement aUpdated = advertisementDB.GetAllAds().Last();

            advertisementDB.RemoveAd(aUpdated.Id);

            Assert.AreEqual(aUpdated.Description, "B");
            Assert.AreEqual(aUpdated.Description, "B");

        }

        [TestMethod]
        public void TestDeleteAds()
        {
            Advertisement ad = new Advertisement
            {
                Title = "A",
                Description = "A",
                Price = 50,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7),
                BikeId = bicycleDB.GetAllBicycles().Last().ID,
                UserID = userDB.GetAllUser().Last().Id,
            };
            advertisementDB.AddAd(ad);

            Advertisement a = advertisementDB.GetAllAds().Last();

            client.RemoveAd(a.Id);

            Assert.AreNotEqual(advertisementDB.GetAllAds().Last().Id,a.Id);
        }

        #endregion

        #region Booking test

        [TestMethod]
        public void TestAddBooking()
        {

            Advertisement ad = new Advertisement
            {
                Title = "lorem 3",
                Description = "K0WJZFWWZW VLL4262TZI 81CS84SUUR OCWPGNS3X2 66WJ5APZLR BDCHCU3WEC",
                Price = 18.90,
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.AddDays(1).Date,
                UserID = userDB.GetAllUser().Last().Id,
                BikeId = bicycleDB.GetAllBicycles().Last().ID
            };

            advertisementDB.AddAd(ad);
            var currentAd = advertisementDB.GetAllAds().Last();
            

            Booking b = new Booking
            {
                AdvertismentId = currentAd.Id,
                StartDate = currentAd.StartDate,
                EndDate = currentAd.EndDate,
                RentUserId = userDB.GetAllUser().First().Id,
                TotalPrice = 50
            };

            client.CreateBooking(b);

            var booking = bookingDB.GetAllBookings().Last();
            bookingDB.RemoveBooking(booking.Id);
            advertisementDB.RemoveAd(ad.Id);
        }
 
        #endregion

    }
}
