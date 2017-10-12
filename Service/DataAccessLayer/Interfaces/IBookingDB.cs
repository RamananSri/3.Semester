using System.Collections.Generic;
using ModelLayer;

namespace DataAccessLayer
{
    public interface IBookingDB
    {
        // CREATE

        void CreateBooking(Booking booking);

        // READ

        Booking FindBooking(int bookingId);

        List<Booking> GetBookingsByUser(int userId);

        Booking GetBookingByPrice(double price);

        List<Booking> GetAllBookings();

        // REMOVE

        void RemoveBooking(int bookingId);


    }
}