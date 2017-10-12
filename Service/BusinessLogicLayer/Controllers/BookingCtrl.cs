using System;
using System.Collections.Generic;
using System.ServiceModel;
using DataAccessLayer;
using ModelLayer;

namespace BusinessLogicLayer
{
    public class BookingCtrl
    {
        private readonly IBookingDB _bDB;

        public BookingCtrl()
        {
            _bDB = new BookingDB();
        }

        public BookingCtrl(IBookingDB bookingDB)
        {
            _bDB = bookingDB;
        }

        public void CreateBooking(Booking b)
        {

            if (b.TotalPrice < 0)
            {
                throw new FaultException("price error");
            }

            if (!ValidateDate(b.StartDate,b.EndDate))
            {
                throw new FaultException("Please check if your dates are set correctly");
            }

            try
            {
                _bDB.CreateBooking(b);
            }
            catch (FaultException) { throw; }
        }

        public void RemoveBooking(int bookingId)
        {
            _bDB.RemoveBooking(bookingId);
        }

        public List<Booking> GetBookingsByUser(int userId)
        {
            try
            {
                // ?? = null-coalescing operator
                List<Booking> bookings = _bDB.GetBookingsByUser(userId) ?? new List<Booking>();

                return bookings;
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
                return _bDB.GetBookingByPrice(price);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public double CalcPrice(DateTime startDate, DateTime endDate, double price)
        {
            if (ValidateDate(startDate,endDate) && price > 0)
            {
                double result = (endDate - startDate).TotalDays + 1;
                return result * Convert.ToDouble(price);
            }
            throw new FaultException("Date error");
        }

        private bool ValidateDate(DateTime start, DateTime end)
        {
            return start.Date >= DateTime.Now.Date && end.Date >= start.Date;
        }

    }
}