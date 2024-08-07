using System.ComponentModel.DataAnnotations;
using MarketPlace.Data.DTO.Site;

namespace MarketPlace.Data.DTO.Account
{
	public class LoginUserDTO : CaptchaViewModel
    {
		[Display(Name = "تلفن همراه")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
		public string Mobile { get; set; }


		[Display(Name = "کلمه ی عبور")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
		public string Password { get; set; }
        public bool RememberMe { get; set; }
       // public string Captcha { get; set; }
    }

	public enum LoginUserResult
	{
		Success,
		NotActivated,
		NotFound
	}
}
