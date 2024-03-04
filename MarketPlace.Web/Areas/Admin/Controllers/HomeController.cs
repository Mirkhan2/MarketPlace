using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        #region Index
        public IActionResult Index()
        {

            return View();
        }
        #endregion
    }
}
