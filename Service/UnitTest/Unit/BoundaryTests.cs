using System;
using System.ServiceModel;
using BusinessLogicLayer;
using DataAccessLayer;
using DataAccessLayer.Modules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLayer;
using Moq;

namespace UnitTest
{
    [TestClass]
    public class BoundaryTests
    {

        private Brand b1, b2, b3;
        private BicycleType bt1, bt2;
        private Frame f1, f2, f3, f4;
        private Wheel w1, w2, w3, w4;
        private User u1, u2;
        private Bicycle bike1, bike2, bike3, bike4, bike5, bike6;
        private Advertisement a1, a2;

        private Mock<IBookingDB> _mockBookingDB;
        private BookingCtrl _bCtrl;

        [TestInitialize]
        public void Setup()
        {
            _mockBookingDB = new Mock<IBookingDB>();                             // mocked database object           
            _mockBookingDB.Setup(m => m.CreateBooking(It.IsAny<Booking>()));     // createBooking på mock tager imod enhver booking
            _bCtrl = new BookingCtrl(_mockBookingDB.Object);                      // injection af mock i vores bookingCtrl

            CryptoModule crypto = new CryptoModule();

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
                Address = "Sofiendalsvej",
                Age = "24",
                Salt = crypto.GenerateSaltString(),
                Email = "brian@gmail.com",
                Name = "Brian",
                PWord = "123",
                Phone = "12345678",
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
        public void Cleanup()
        {
        }

        [TestMethod]
        public void TestNegativePrice()
        {

            Booking b = new Booking
            {
                EndDate = DateTime.Now.Date,
                StartDate = DateTime.Now.Date,
                TotalPrice = -1
            };

            try
            {
                _bCtrl.CreateBooking(b);
                Assert.Fail();
            }
            catch (FaultException e)
            {
                Assert.AreEqual(e.Message, "price error");
            }

        }

        [TestMethod]
        public void TestBookingPriceZero()
        {
            Booking b = new Booking
            {
                EndDate = DateTime.Now.Date,
                StartDate = DateTime.Now.Date,
                TotalPrice = 0
            };
            _bCtrl.CreateBooking(b);
        }

        [TestMethod]
        public void TestBookingPricePositive()
        {
            Booking b = new Booking
            {
                EndDate = DateTime.Now.Date,
                StartDate = DateTime.Now.Date,
                TotalPrice = 1
            };
            _bCtrl.CreateBooking(b);
        }

        [TestMethod]
        public void TestBookingStartDateEqualsEndDate()
        {
            Booking b = new Booking
            {
                EndDate = DateTime.Now.Date,
                StartDate = DateTime.Now.Date,
                TotalPrice = 1
            };
            _bCtrl.CreateBooking(b);
        }

        [TestMethod]
        public void TestBookingStartDateAfterEndDate()
        {
            Booking b = new Booking
            {
                EndDate = DateTime.Now.Date,
                StartDate = DateTime.Now.Date.AddDays(1),
                TotalPrice = 1
            };

            try
            {
                _bCtrl.CreateBooking(b);
            }
            catch (FaultException e)
            {
                Assert.AreEqual(e.Message, "Please check if your dates are set correctly");
            }
        }

        [TestMethod]
        public void TestBookingStartDateBeforeEndDate()
        {
            Booking b = new Booking
            {
                EndDate = DateTime.Now.Date.AddDays(1),
                StartDate = DateTime.Now.Date,
                TotalPrice = 1
            };
            _bCtrl.CreateBooking(b);
        }


        // lav en 2 bookings på samme tid
        // Lav en booking der er udenfor ad tid

    }
}
