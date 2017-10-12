using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ModelLayer;

namespace DataAccessLayer
{
    public class BicycleDB : IBicycleDB
    {
        public Bicycle Find(int? id)
        {
            try
            {
                using (var db = new Context())
                {
                    var b = db.Bikes
                        .Where(a => a.ID == id)
                        .Include(a => a.Brand)
                        .Include(a => a.FrameSize)
                        .Include(a => a.Type)
                        .Include(a => a.WheelSize)
                        .FirstOrDefault();
                    return b;
                }
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public List<Bicycle> GetAllBicycles()
        {
            try
            {
                using (var db = new Context())
                {

                    List<Bicycle> bikes = db.Bikes
                        .Include(a => a.Brand)
                        .Include(a => a.FrameSize)
                        .Include(a => a.Type)
                        .Include(a => a.WheelSize)
                        .ToList();

                    if (bikes.Equals(null))
                    {
                        return null;
                    }
                    return bikes;
                }


            }
            catch (Exception)
            {
                throw;
            }
        }

        //TODO: kommenter kode goodie rapport
        public List<Bicycle> GetBikesByUser(int id)
        {
            try
            {
                using (var db = new Context())
                {
                    //                var UserID = db.Users
                    //                    .Where(e => e.Name == userName)
                    //                    .Select(e => e.Id);

                    if (!db.Bikes.Any())
                    {
                        return null;
                    }

                    List<Bicycle> bikes = db.Bikes
                        .Where(b => b.User.Id == id)
                        .Include(b => b.Brand)
                        .Include(b => b.FrameSize)
                        .Include(b => b.WheelSize)
                        .Include(b => b.Type)
                        .ToList();

                    //                var bikes = db.Bikes
                    //                    .Where(b => b.User.Name == userName)
                    //                    .ToList();

                    return bikes;
                    //                return null;
                }
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
                using (var db = new Context())
                {
                    List<Brand> brands = db.Brands.ToList();
                    return brands;
                }
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
                using (var db = new Context())
                {
                    List<BicycleType> bicycleTypes = db.BicycleTypes.ToList();

                    return bicycleTypes;
                }
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
                using (var db = new Context())
                {
                    List<Wheel> WheelSizes = db.WheelSizes.ToList();
                    return WheelSizes;
                }
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
                using (var db = new Context())
                {
                    List<Frame> FrameSizes = db.FrameSizes.ToList();
                    return FrameSizes;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateBicyle(Bicycle b)
        {
            using (var db = new Context())
            {
                db.Bikes.Add(b);
                db.SaveChanges();
            }
        }

        public Bicycle GetBikeByYear(string year)
        {
            try
            {
                using (var db = new Context())
                {
                    Bicycle b = db.Bikes
                        .Where(i => i.Year == year)
                        .OrderByDescending(i => i.ID)
                        .FirstOrDefault();
                    return b;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveBicycle(int bicycleId)
        {
            using (Context db = new Context())
            {
                Bicycle b = Find(bicycleId);

                if (b != null)
                {
                    db.Bikes.Attach(b);
                    db.Bikes.Remove(b);
                    db.SaveChanges();
                }
            }
        }
    }
}