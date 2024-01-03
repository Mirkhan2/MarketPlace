using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Controllers
{
	public class HomeController : SiteBaseController
	{
		public IActionResult Index()
		{
			return View();
		}

		
	}
}
