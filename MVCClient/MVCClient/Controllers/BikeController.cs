using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MVCClient.Service;
using MVCClient.ViewModel;

namespace MVCClient.Controllers
{
    public class BikeController : Controller
    {
        private BikeServiceClient client = new BikeServiceClient();

        // GET: Bike
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult ViewBikes()
        {
            var id = Session["ID"];
            List<Bicycle> bikes = client.GetBikesByUser(Convert.ToInt32(id));

//            BikeDetails bikeViewBag = new BikeDetails
//            {
//                bikes = bikes
//            };
         
            return View(bikes);
        }

        [HttpGet]
        public ActionResult CreateBike()
        {

            Console.WriteLine("hej fra dropdown fyld");
//            CreateBike cs = new CreateBike();
            

            List<Brand> b = client.GetBrands();
            List<BicycleType> t = client.GetTypes();
            List<Wheel> w = client.GetWheelSizes();
            List<Frame> f = client.GetFrameSizes();

            List<SelectListItem> Brands = new List<SelectListItem>();
            List<SelectListItem> Types = new List<SelectListItem>();
            List<SelectListItem> WheelSize = new List<SelectListItem>();
            List<SelectListItem> FrameSize = new List<SelectListItem>();


            foreach (var VARIABLE in b)
            {
                Brands.Add(new SelectListItem {Text = VARIABLE.Name, Value = VARIABLE.Id.ToString()});    
            }

            foreach (var VARIABLE in t)
            {
                Types.Add(new SelectListItem { Text = VARIABLE.TypeName, Value = VARIABLE.Id.ToString()});
            }

            foreach (var VARIABLE in w)
            {
                WheelSize.Add(new SelectListItem { Text = VARIABLE.Size.ToString(), Value = VARIABLE.Id.ToString()});
            }

            foreach (var VARIABLE in f)
            {
                FrameSize.Add(new SelectListItem { Text = VARIABLE.Size.ToString(), Value = VARIABLE.Id.ToString() });
            }

            DropdownDetails details = new DropdownDetails {Brands = Brands,Types = Types,Frames = FrameSize,Wheels = WheelSize};

            return View(details);
        }

        [HttpPost]
        public ActionResult CreateBike(string year, DropdownDetails ddd)
        {
            var id = Session["ID"];

            Bicycle b = new Bicycle
            {
                BrandId = ddd.SelectedBrand.Id,
                FrameSizeId = ddd.SelectedFrame.Id,
                TypeId = ddd.SelectedType.Id,
                UserId = Convert.ToInt32(id),
                WheelSizeId = ddd.SelectedWheel.Id,
                Year = year
            };

            client.CreateBicycle(b);

            return RedirectToAction("ViewBikes", "Bike");
        }

        //[HttpPost]
        //public ActionResult CreateBike(DropdownDetails dropDown, string year)
        //{
//TODO mindske service kald
	        //var id = Session["ID"];
//			BicycleType bicycleType =  client.GetTypes().Find(a=> a.TypeName== Types);
//
//			Brand brands = client.GetBrands().Find(a => a.Name == Brands);
//	        Wheel wheelSize = client.GetWheelSizes().Find((a => a.Size == Convert.ToInt32(WheelSize)));
//	        Frame frameSize = client.GetFrameSizes().Find(a => a.Size == Convert.ToInt32(FrameSize));

//            client.CreateBicycle(year,dropDown.SelectedBrand,dropDown.SelectedType,dropDown.SelectedWheel,dropDown.SelectedFrame,Convert.ToInt32(id));
            //            Console.WriteLine(year, brands, bicycleType, wheelSize, frameSize, Convert.ToInt32(id));



			//return View(dropDown);
   //     }

        public ActionResult DeleteBike(int ID)
        {
            client.RemoveBicycle(ID);

            return RedirectToAction("ViewBikes");
        }

        public ActionResult UpdateBike(string ID)
        {
            ViewBag.ID = ID;

            return View();

        }



    }
}