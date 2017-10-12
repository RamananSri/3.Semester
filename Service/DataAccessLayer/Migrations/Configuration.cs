using System;
using DataAccessLayer.Modules;
using ModelLayer;

namespace DataAccessLayer.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<Context>
    {

        private CryptoModule crypto;

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            crypto = new CryptoModule();
        }

        protected override void Seed(Context context)
        {

            // Seed Brands
            Brand b1 = new Brand { Name = "Croissant" };
            Brand b2 = new Brand { Name = "Canondale" };
            Brand b3 = new Brand { Name = "Trek" };

            // Seed Types
            BicycleType bt1 = new BicycleType { TypeName = "Mountainbike" };
            BicycleType bt2 = new BicycleType { TypeName = "Racer" };

            // Seed FrameSizes
            Frame f1 = new Frame { Size = 30 };
            Frame f2 = new Frame { Size = 32 };
            Frame f3 = new Frame { Size = 34 };
            Frame f4 = new Frame { Size = 36 };

            // Seed WheelSizes
            Wheel w1 = new Wheel { Size = 20 };
            Wheel w2 = new Wheel { Size = 22 };
            Wheel w3 = new Wheel { Size = 24 };
            Wheel w4 = new Wheel { Size = 26 };

            // Seed Users

            var salt1 = crypto.GenerateSaltString();
            var salt2 = crypto.GenerateSaltString();

            User u1 = new User
            {
                Address = "Sofiendalsvej",
                Age = "24",
                Salt = salt2,
                Email = "brian@gmail.com",
                Name = "Brian",
                PWord = crypto.HashPassword("123",salt2),
                Phone = "12345678",
                Zipcode = "9200"
            };

            User u2 = new User
            {
                Address = "Hobrovej",
                Age = "30",
                Salt = salt1,
                Email = "john@gmail.com",
                Name = "John",
                PWord = crypto.HashPassword("123",salt1),
                Phone = "87654321",
                Zipcode = "9000"
            };

            // Seed bicycles

            Bicycle bike1 = new Bicycle
            {
                Brand = b1,
                FrameSize = f1,
                Type = bt1,
                User = u1,
                WheelSize = w1,
                Year = "2017"
            };
            context.Bikes.Add(bike1);

            Bicycle bike2 = new Bicycle
            {
                Brand = b2,
                FrameSize = f2,
                Type = bt2,
                User = u1,
                WheelSize = w2,
                Year = "2016"
            };

            Bicycle bike3 = new Bicycle
            {
                Brand = b3,
                FrameSize = f3,
                Type = bt1,
                User = u1,
                WheelSize = w3,
                Year = "2015"
            };
            context.Bikes.Add(bike3);

            Bicycle bike4 = new Bicycle
            {
                Brand = b3,
                FrameSize = f1,
                Type = bt1,
                User = u2,
                WheelSize = w1,
                Year = "2012"
            };

            Bicycle bike5 = new Bicycle
            {
                Brand = b3,
                FrameSize = f3,
                Type = bt1,
                User = u2,
                WheelSize = w3,
                Year = "2011"
            };
            context.Bikes.Add(bike5);

            Bicycle bike6 = new Bicycle
            {
                Brand = b3,
                FrameSize = f4,
                Type = bt1,
                User = u2,
                WheelSize = w4,
                Year = "2010"
            };
            context.Bikes.Add(bike6);

            // Seed advertisements

            context.Ads.Add(new Advertisement
            {
                Title = "lorem 2",
                Description = "K0WJZFWWZW VLL4262TZI 81CS84SUUR OCWPGNS3X2 66WJ5APZLR BDCHCU3WEC",
                Price = 18.90,
                StartDate = DateTime.ParseExact("01/05/2017", "dd/MM/yyyy", null),
                EndDate = DateTime.ParseExact("01/07/2017", "dd/MM/yyyy", null),
                User = u2,
                Bike = bike4
            });

            context.Ads.Add(new Advertisement
            {
                Title = "lorem 1",
                Description = "K0WJZFWWZW VLL4262TZI 81CS84SUUR OCWPGNS3X2 66WJ5APZLR BDCHCU3WEC",
                Price = 18.90,
                StartDate = DateTime.ParseExact("01/05/2017", "dd/MM/yyyy", null),
                EndDate = DateTime.ParseExact("01/07/2017", "dd/MM/yyyy", null),
                User = u1,
                Bike = bike2

            });
        }
    }
}
