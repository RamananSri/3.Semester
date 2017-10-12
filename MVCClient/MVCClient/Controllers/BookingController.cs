using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web.Mvc;
using MVCClient.Service;

namespace MVCClient.Controllers
{
    public class BookingController : Controller
    {

        private BikeServiceClient client = new BikeServiceClient();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateBooking(int id, DateTime inputSDate, DateTime inputEDate)
        {
            ViewBag.FaultEx = "";
            try
            {
                Advertisement ad = client.FindAdById(id);
            var userId = Session["ID"];
            double result = client.CalcPrice(inputSDate, inputEDate, ad.Price);

            Booking b = new Booking
            {
                AdvertismentId = id,
                EndDate = inputEDate,
                RentUserId = Convert.ToInt32(userId),
                StartDate = inputSDate,
                TotalPrice = result,
            };

            
                client.CreateBooking(b);
                return RedirectToAction("SearchAds", "Ads");
            }

            catch (FaultException e)
            {
                ViewBag.FaultEx = e.Message;
                return RedirectToAction("ViewAd", "Ads", new {ID=id});
            }

            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }


        public ActionResult ViewBookings()
        {
            var userId = Session["ID"];
            List<Booking> bookings = client.GetBookingsByUser(Convert.ToInt32(userId));

            if (bookings == null)
            {
                bookings = new List<Booking>();
            }
            
            return View(bookings);
        }

        public ActionResult DeleteBooking(int id)
        {
            client.RemoveBooking(id);

            return RedirectToAction("Index", "Home");
        }
    }
}