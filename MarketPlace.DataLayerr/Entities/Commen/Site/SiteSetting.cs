

using System.ComponentModel.DataAnnotations;

namespace MarketPlace.DataLayerr.Entities.Commen.Site
{
    public class SiteSetting : BaseEntity
    {
        #region properties

        [Display(Name = "تلفن همراه")]
        public string Mobile { get; set; }

        [Display(Name = "تلفن")]
        public string Phone { get; set; }

        [Display(Name = "آدرس ایمیل")]
        public string Email { get; set; }

        [Display(Name = "متن فوتر")]
        public string FooterText { get; set; }

        [Display(Name = "متن کپی رایت")]
        public string CopyRight { get; set; }

        [Display(Name = "آدرس نقشه")]
        public string MapScript { get; set; }

        [Display(Name = "آدرس")]
        public string Address { get; set; }

        [Display(Name = "اصلی هست / نیست")]
        public bool IsDefault { get; set; }

        #endregion
    }
}
