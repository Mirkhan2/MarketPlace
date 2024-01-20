using System.Threading.Tasks;
using GoogleReCaptcha.V3.Interface;
using MarketPlace.Applicationn.Services.Interfaces;
using MarketPlace.DataLayerr.DTO.Contacts;
using MarketPlace.Web.PresentationExtensions;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Controllers
{
	public class HomeController : SiteBaseController
	{
		#region constructor

		private readonly IContactService _contactService;
		private readonly ICaptchaValidator _captchaValidator;

		public HomeController(IContactService contactService, ICaptchaValidator captchaValidator)
		{
			_contactService = contactService;
			_captchaValidator = captchaValidator;
		}

		#endregion

		#region index

		public async Task<IActionResult> Index()
		{
			return View();
		}

		#endregion

		#region contact us

		[HttpGet("contact-us")]
		public async Task<IActionResult> ContactUs()
		{
			return View();
		}

		[HttpPost("contact-us"), ValidateAntiForgeryToken]
		public async Task<IActionResult> ContactUs(CreateContactUsDTO contact)
		{
			if (!await _captchaValidator.IsCaptchaPassedAsync(contact.Captcha))
			{
				TempData[ErrorMessage] = "کد کپچای شما تایید نشد";
				return View(contact);
			}

			if (ModelState.IsValid)
			{
				var ip = HttpContext.GetUserIp();
				await _contactService.CreateContactUs(contact, HttpContext.GetUserIp(), User.GetUserId());
				TempData[SuccessMessage] = "پیام شما با موفقیت ارسال شد";
				return RedirectToAction("ContactUs");
			}

			return View(contact);
		}

		#endregion


	}
}
