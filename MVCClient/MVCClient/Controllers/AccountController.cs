using MVCClient.Service;
using System;
using System.Web.Mvc;
using MVCClient.ViewModel;

//using MVCClient.AccountServiceClient;

namespace MVCClient.Controllers
{
    public class AccountController : Controller
    {
        private BikeServiceClient client = new BikeServiceClient();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string inputEmail, string inputPassword, string inputName, string inputPhone, string inputAddress, string inputZipcode, string inputAge)
        {
	  //      if (!ModelState.IsValid)
	  //      {
			//	return View();
			//}
	        
            try
            {
                client.CreateUser(inputEmail, inputPassword, inputName, inputPhone, inputAddress, inputZipcode, inputAge);
            }
            catch (Exception)
            {
                ViewBag.Message = "error, try again later";
            }
            return View();
        }

        public ActionResult EditUser()
        {
           var id = Session["ID"];
           var user = client.GetUser(int.Parse(id.ToString()));
           var userViewbag = new UserDetails()
            {
                ID =  id.ToString(),
                Email = Session["Email"].ToString(),
                Name = user.Name,
                Phone = user.Phone,
                Address = user.Address,
                Zipcode = user.Zipcode,
                Age = user.Age
            };
            return View(userViewbag);
        }
        
        [HttpPost]
        public ActionResult EditUser(string inputEmail, string inputName, string inputPhone, string inputAddress, string inputZipcode, string inputAge)
        {
			//if(!ModelState.IsValid)
			//return View();
            var ID = Session["ID"];
            client.ModifyUser(Convert.ToInt32(ID), inputEmail, inputName, inputPhone, inputAddress, inputZipcode, inputAge);
            return RedirectToAction("Index", "Ads");
        }

        [HttpPost]
        public ActionResult DeleteUser()
        {
            var id = Session["ID"];
            client.RemoveUser(Convert.ToInt32(id));
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string inputEmail, string inputPassword)
        {
            User user = client.LoginUser(inputEmail, inputPassword);
            if (user != null)
            {
                try
                {
                    if (inputEmail.Equals(user.Email))
                    {
                        Session["Email"] = inputEmail;
                        Session["ID"] = user.Id;
                        return RedirectToAction("Index", "Ads");
                    }
                }
                //catch hvis de skriver forkert brugernavn/kode
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            else
            {
                ViewBag.Error = "der er sket en fejl";
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}