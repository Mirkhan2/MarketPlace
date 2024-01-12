using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayerr.DTO.Site
{
    public class CaptchaViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Capthcha { get; set; }
    }
}
