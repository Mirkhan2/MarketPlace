using System.Threading.Tasks;
using MarketPlace.App.Services.Interfaces;
using MarketPlace.Data.DTO.Common;
using MarketPlace.Data.DTO.Seller;
using MarketPlace.Web.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Areas.Admin.Controllers
{
    public class SellerController : AdminBaseController
    {
        #region contructor
        private readonly ISellerService _sellerService;
        public SellerController(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }



        #endregion

        #region seller request

        public async Task<IActionResult> SellerRequests(FilterSellerDTO filter)
        {
            return View(await _sellerService.FilterSellers(filter));
        }

        #endregion

        #region accept seller request
        [HttpPost , ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptSellerRequest(long requestId)
        {
            var result = await _sellerService.AcceptSellerRequest(requestId);

            if (result)
            {
                return JsonResponseStatus.SendStatus(JsonResponsStatusType.Success,
                    "Request Seccussfully " , null);

            }
 

            return JsonResponseStatus.SendStatus(JsonResponsStatusType.Danger,
                "Finde ich mit diese information leider nicht " , null);
        }
        #endregion

        #region rejected seller request
        [HttpPost]
        public async Task<IActionResult> RejectSellerRequest(RejectItemDTO reject)
        {
            var result = await _sellerService.RejectSellerRequest(reject);

            if (result)
            {
                return JsonResponseStatus.SendStatus(JsonResponsStatusType.Success,
                    "Request Seccussfully ", null);
            }

            return JsonResponseStatus.SendStatus(JsonResponsStatusType.Danger,
                "Finde ich mit diese information leider nicht ", null);
        }
        #endregion
    }
}
