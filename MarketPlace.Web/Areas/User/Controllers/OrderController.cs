using System.Threading.Tasks;
using MarketPlace.App.Services.Interfaces;
using MarketPlace.Data.DTO.Orders;
using MarketPlace.Web.Http;
using MarketPlace.Web.PresentationExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Areas.User.Controllers
{
    public class OrderController : UserBaseController
    {
        #region constructor

        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        public OrderController(IOrderService orderService , IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }
        #endregion

        #region add product to open order
        [HttpPost("add-product-to-order")]
        [AllowAnonymous]
        public async Task<IActionResult> AddProductToOrder(AddProductToOrderDTO order)
        {
            if (!ModelState.IsValid)
            {
               if(User.Identity.IsAuthenticated)
                {
                    await _orderService.AddProductToOpenOrder(User.GetUserId(), order);
                    return JsonResponseStatus.SendStatus(JsonResponsStatusType.Success,
                        "Product Succed submit",
                        null);
                }
                else
                {
                    return JsonResponseStatus.SendStatus(JsonResponsStatusType.Danger,
                    "Product  Error First site Handeln",
                    null);

                }
            }
            return JsonResponseStatus.SendStatus(JsonResponsStatusType.Danger,
                "Error", null);
        }

        #endregion
    }
}
