using System.ComponentModel.DataAnnotations;

namespace MarketPlace.DataLayerr.DTO.Site
{
    public class CaptchaViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Captcha { get; set; }
    }
}