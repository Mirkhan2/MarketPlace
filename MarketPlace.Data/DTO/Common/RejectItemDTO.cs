using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Data.DTO.Common
{
    public  class RejectItemDTO
    {
        public long Id { get; set; }
        [Display(Name = "توضیحات تایید / عدم تایید اطلاعات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string RejectedMessage { get; set; }
    }
}
