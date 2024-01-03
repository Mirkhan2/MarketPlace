using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Controllers
{
	public class SiteBaseController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
