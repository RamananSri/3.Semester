using System.Collections.Generic;
using ModelLayer;
using System.Data.Entity;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Transactions;

namespace DataAccessLayer
{
    public class AdvertisementDB : IAdvertisementDB
    {
        private BookingDB _bDb;

        public Advertisement FindAdById(int id)
        {
            try
            {
                Advertisement ad;

                using (var db = new Context())
                {
                    ad = db.Ads
                        .Where(i => i.Id == id)
                        .Include(i => i.Bike)
                        .Include(i => i.Bike.Brand)
                        .Include(i => i.Bike.FrameSize)
                        .Include(i => i.Bike.Type)
                        .Include(i => i.Bike.WheelSize)
                        .FirstOrDefault();
                }
                return ad;
            }

            catch (Exception){ throw; }        
        }

        public void ModifyAd(Advertisement a)   
        {
            using (Context db = new Context())
            {
                var original = db.Ads.Find(a.Id);

                if (original != null)
                {
                    db.Entry(original).CurrentValues.SetValues(a);
                    db.SaveChanges();
                }
            }
        }

        public void RemoveAd(int id)
        {
            using (Context db = new Context())
            {
                Advertisement ad = db.Ads.Find(id);

                if (ad != null)
                {
                    db.Ads.Remove(ad);
                    db.SaveChanges();
                }
            }
        }

        public List<Advertisement> GetAdvertisementsByUser(int id)
        {

            TransactionOptions transactionOptions = new TransactionOptions{IsolationLevel = IsolationLevel.ReadUncommitted};
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,transactionOptions))
            {
                try
                {
                    using (Context db = new Context())
                    {
                        List<Advertisement> advertisements = db.Ads
                            .Where(a => a.UserID == id)
                            .Include(a => a.Bike)
                            .Include(a => a.Bike.Brand)
                            .Include(a => a.Bike.Type)
                            .Include(a => a.Bike.FrameSize)
                            .Include(a => a.Bike.WheelSize)
                            .ToList();

                        scope.Complete();

                        if (advertisements.Equals(null))
                        {
                            throw new FaultException("Advertisement not found");
                        }
                        return advertisements;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        
        public List<Advertisement> GetAllAds()
        {
            try
            {
                using (var db = new Context())
                {
                    List<Advertisement> advertisements = db.Ads
                            .Include(a => a.Bike)
                            .Include(a => a.Bike.Brand)
                            .Include(a => a.Bike.Type)
                            .Include(a => a.Bike.FrameSize)
                            .Include(a => a.Bike.WheelSize)
                            .ToList();

                    if (advertisements.Equals(null))
                    {
                        return null;
                    }
                    return advertisements;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Advertisement> GetAvailableAds(DateTime start, DateTime end)
        {
            _bDb = new BookingDB();
            List<Advertisement> finalAdvertisements = new List<Advertisement>();

            using (Context db = new Context())
            {
                var ads = db.Ads.Where(a => a.StartDate >= start && a.EndDate <= end);

                foreach (var ad in ads)
                {
                    if (_bDb.AdAvailability(start, end, ad.Id, db))
                    {
                        finalAdvertisements.Add(ad);
                    }
                }
            }
            return finalAdvertisements;
        }

        public void AddAd(Advertisement ad)
        {
            try
            {
                using (Context db = new Context())                  // Using: Context object garbage collectes efter den er færdig
                {
                    db.Ads.Add(ad);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Advertisement GetAdByTitle(string title)
        {
            try
            {
                using (Context db = new Context())
                {
                    Advertisement ad = db.Ads
                        .Where(i => i.Title == title)
                        .OrderByDescending(i => i.Id)
                        .FirstOrDefault();
                    return ad;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

//        public List<Advertisement> GetAdsByTimespan(List<DateTime> span)
//        {
//            using (Context db = new Context())
//            {
//                List<Advertisement> ads = new List<Advertisement>();
//
//                foreach (var dateTime in span)
//                {
//                    var ad = db.Ads
//                        .Where(a => a.StartDate == dateTime.Date && a.EndDate == dateTime.Date);
//
//
//                    ads.Add(ad);
//
//                }
//            }
//        }

        public void RemoveAdsByBikeId(int bicycleId)
        {
            using (var db = new Context())
            {
                var ads = db.Ads
                    .Where(a => a.BikeId == bicycleId)
                    .ToList();

                foreach (var ad in ads)
                {
                    db.Ads.Remove(ad);
                    db.SaveChanges();
                }
            }
        }

    }
}