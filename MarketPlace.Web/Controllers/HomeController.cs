using System.Threading.Tasks;
using MarketPlace.Applicationn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Controllers
{
	public class HomeController : SiteBaseController
	{
		#region

		private readonly ISmsService _smsService;
        public HomeController(ISmsService smsService)
        {
            _smsService = smsService;
        }
        #endregion
        public async Task<IActionResult> Index()
		{
		//	await _smsService.SendSms();
			return View();
		}

		
	}
}
