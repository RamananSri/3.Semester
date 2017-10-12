using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MVCClient.ViewModel;
using MVCClient.Service;

namespace MVCClient.Controllers
{
    public class AdsController : Controller
    {
        private BikeServiceClient client = new BikeServiceClient();

        // GET: Ads
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateAd()
        {
            var id = Session["ID"];
            List <Bicycle> b = client.GetBikesByUser(Convert.ToInt32(id));

            List<SelectListItem> Bicycles = new List<SelectListItem>();

            foreach (var VARIABLE in b)
            {
                Bicycles.Add(new SelectListItem { Text = VARIABLE.Year, Value = VARIABLE.ID.ToString() });
            }

            BikeDetails ddBikes = new BikeDetails { Bicycles = Bicycles };

            return View(ddBikes);
        }

        [HttpPost]
        public ActionResult CreateAd(string inputTitle, string inputDesc, string inputPrice, DateTime? inputSDate, DateTime? inputEDate, BikeDetails ddBike)
        {
            var selectedBike = ddBike.SelectedBike.ID;
            var id = Session["ID"];
            try
            {
                client.CreateAd(inputTitle, inputDesc, Convert.ToDouble(inputPrice), Convert.ToDateTime(inputSDate), Convert.ToDateTime(inputEDate), selectedBike, Convert.ToInt32(id));
                return RedirectToAction("Index", "Ads");
            }
            catch (Exception e)
            {
                TempData["AlertMessage"] = e.Message;
                return RedirectToAction("Index", "Ads");
            }
        }

        [HttpPost]
        public ActionResult DeleteAd(int id)
        {
            client.RemoveAd(id);
            return RedirectToAction("Index", "Ads");
        }

        public ActionResult ViewAds()
        {
            var id = Session["ID"];
            List<Advertisement> ads = client.GetAdvertisementsByUser(Convert.ToInt32(id));

//            List<AdvertisementDetails> adss = new List<AdvertisementDetails>();
//
//            foreach (var ad in ads)
//            {
//                adss.Add(new AdvertisementDetails {Advertisement = ad,Bicycle = ad.Bike});
//            }


            return View(ads);
        }
        [HttpGet]
        public ActionResult ViewAd(int ID)
        {

            if (ID.Equals(null))
            {
               throw new NotImplementedException();
            }

            Advertisement ad = client.FindAdById(ID);

            return View(ad);
        }

        public ActionResult ViewBtnClicked()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ViewBtnClicked(int adId)
        {

            if (adId.Equals(null))
            {
                throw new NotImplementedException();
            }

            Advertisement ad2 = client.FindAdById(adId);

            return View(ad2);
        }

        public ActionResult SearchAds(string SearchString)
        {
            // TODO: søg på zipcode (searchString) 

            List<Advertisement> ads = client.GetAllAds();

            return View(ads);
        }


        public ActionResult AvailableSearch(DateTime inputSDate, DateTime inputEDate)
        {
            List<Advertisement> ads = client.GetAvailableAds(inputSDate, inputEDate);
            
            return View("SearchAds",ads);
        }
    }
}