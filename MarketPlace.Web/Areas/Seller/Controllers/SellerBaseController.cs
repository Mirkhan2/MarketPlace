using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Areas.Seller.Controllers
{
    [Authorize]
    [Area("seller")]
    [Route("seller")]
    public class SellerBaseController : Controller
    {
       
    }
}
