using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Data.Entities.Commen;
using MarketPlace.Data.Entities.ProductOrder;

namespace MarketPlace.Data.Entities.Products
{
    public class ProductColor : BaseEntity
    {
        #region properties

        public long ProductId { get; set; }

        [Display(Name = "رنگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ColorName { get; set; }
        [Display(Name = "رنگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string ColorCode { get; set; }

        public int Price { get; set; }

        #endregion

        #region relations

        public Product Product { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }


        #endregion
    }
}
