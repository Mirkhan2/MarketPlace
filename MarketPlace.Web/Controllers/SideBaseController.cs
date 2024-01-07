using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Controllers
{
	public class SideBaseController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
