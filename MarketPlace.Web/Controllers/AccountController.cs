using System.Threading.Tasks;
using MarketPlace.Application.Services.Implementations;
using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayerr.DTOs.Account;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Controllers
{
	public class AccountController : SiteBaseController
	{
		#region constructor

		private readonly IUserService _userService;

		public AccountController(IUserService userService)
		{
			_userService = userService;
		}

		#endregion

		#region register

		[HttpGet("register")]
		public IActionResult Register()
		{
			
			return View();
		}

		[HttpPost("register"), ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterUserDTO register)
		{
			if (ModelState.IsValid)
			{
				var res = await _userService.RegisterUser(register);
				switch (res)
				{
					case RegisterUserResult.MobileExists:
						TempData[ErrorMessage] = "تلفن همراه وارد شده تکراری می باشد";
						ModelState.AddModelError("Mobile", "تلفن همراه وارد شده تکراری می باشد");
						break;
					case RegisterUserResult.Success:
						TempData[SuccessMessage] = "ثبت نام شما با موفقیت انجام شد";
						TempData[InfoMessage] = "کد تایید تلفن همراه برای شما ارسال شد";
						return RedirectToAction("Login");
				}
			}

			return View(register);
		}

		#endregion

		#region login

		[HttpGet("Login")]
		public IActionResult Login()
		{
			return View();
		}
		public async Task<IActionResult> Loginn()
		{
			return View();
		}
		#endregion
	}
}
