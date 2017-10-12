using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Transactions;
using DataAccessLayer.Logging;
using ModelLayer;

namespace DataAccessLayer
{
    public class BookingDB : IBookingDB
    {
        private Logger log;
        private AdvertisementDB advertisementDB;

        public BookingDB()
        {
            log = new Logger();
        }

        public void CreateBooking(Booking booking)
        {
            TransactionOptions option = new TransactionOptions();
            option.IsolationLevel = IsolationLevel.Serializable;

            using (var scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                advertisementDB = new AdvertisementDB();
                try
                {
                    using (var db = new Context())
                    {
                        var chosenAd = advertisementDB.FindAdById(booking.AdvertismentId);

                        if (chosenAd.StartDate.Date <= booking.StartDate.Date && 
                            booking.EndDate <= chosenAd.EndDate.Date)
                        {
                            var canBook = AdAvailability(booking.StartDate.Date, booking.EndDate.Date, 
                                booking.AdvertismentId, db);
                            if (canBook)
                            {
                                db.Bookings.Add(booking);
                                db.SaveChanges();
                                scope.Complete();
                            }
                            else
                            {
                                log.WriteToLog(0, null, "Timespan already booked.Booking cancelled.Please pick another date");
                                throw new FaultException("Timespan already booked. Booking cancelled. Please pick another date");
                            }
                        }
                        else
                        {
                            log.WriteToLog(0, null, "Ad not available. Booking cancelled. Please pick another date");
                            throw new FaultException("Ad not available. Booking cancelled. Please pick another date");
                        }
                    }
                }
                catch (SqlException se)
                {
                    log.WriteToLog(se.Number, se.StackTrace, se.Message);
                    throw new FaultException("Database error");
                }

                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                    {
                        log.WriteToLog(e.HResult, e.InnerException.StackTrace, e.InnerException.Message);
                    }
                    else
                    {
                        log.WriteToLog(e.HResult, e.StackTrace, e.Message);
                    }
                    throw new FaultException("Ad not available. Booking cancelled. Please pick another date");
                }

            }
        }

        public bool AdAvailability(DateTime start, DateTime end, int adId, Context db)
        {
            var canbook = db.Bookings
                .Where(x => x.StartDate >= start &&
                    x.EndDate <= end &&
                    x.AdvertismentId == adId)
                .ToList();

            if (!canbook.Any())
            {
                return true;
            }

            return false;
        }

        public void RemoveBooking(int bookingId)
        {
            using (var db = new Context())
            {

                Booking b = FindBooking(bookingId);

                if (b != null)
                {
                    db.Bookings.Attach(b);
                    db.Bookings.Remove(b);
                    db.SaveChanges();
                }
            }
        }

        public Booking FindBooking(int bookingId)
        {
            try
            {
                Booking b = null;

                using (var db = new Context())
                {
                    b = db.Bookings.Find(bookingId);
                }
                return b;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Booking> GetBookingsByUser(int userId)
        {
            try
            {
                using (var db = new Context())
                {
                    if (!db.Bookings.Any())
                    {
                        return null;
                    }

                    List<Booking> bookings = db.Bookings
                        .Where(b => b.RentUser.Id == userId)
                        .ToList();

                    return bookings;
                }
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
                using (Context db = new Context())
                {
                    Booking b = db.Bookings
                        .Where(i => i.TotalPrice == price)
                        .OrderByDescending(i => i.Id)
                        .FirstOrDefault();
                    return b;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Booking> GetAllBookings()
        {
            List<Booking> bookings;

            using (Context db = new Context())
            {
                bookings = db.Bookings.ToList();
            }

            if (bookings.Equals(null))
            {
                return null;
            }

            return bookings;
        }
    }
}
