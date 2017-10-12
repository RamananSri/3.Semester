using System.Web.Mvc;
using MVCClient.ViewModel;

namespace MVCClient.Controllers
{
    public class PartialController : Controller
    {
        private System.Web.Services.Description.Service client = new System.Web.Services.Description.Service();

        // GET: Partial
        public ActionResult CurrentUserPartial()
        {
            var user = new UserDetails()
            {
                ID = Session["ID"].ToString(),
                Email = Session["Email"].ToString()
            };

            return PartialView("_CurrentUserPartial", user);
        }

        //public ActionResult DeleteUserPartial(int id)
        //{

        //    id = Convert.ToInt32(Session["ID"]);

        //    client.RemoveUser(id);
        //    return PartialView("_DeleteUserPartial");
        //}
    }
}