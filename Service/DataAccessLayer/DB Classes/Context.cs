using System.Data.Entity;
using ModelLayer;

namespace DataAccessLayer
{
    public class Context : DbContext
    {
        public Context() : base("Connection")
        {
            Database.SetInitializer<Context>(new CreateDatabaseIfNotExists<Context>());
        }

        public DbSet<Advertisement> Ads { get; set; }
        public DbSet<Bicycle> Bikes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }


        public DbSet<Brand> Brands { get; set; }
        public DbSet<Wheel> WheelSizes { get; set; }
        public DbSet<Frame> FrameSizes { get; set; }
        public DbSet<BicycleType> BicycleTypes { get; set; }

    }
}