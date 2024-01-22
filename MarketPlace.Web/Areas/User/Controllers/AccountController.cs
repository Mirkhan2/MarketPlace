using System.Threading.Tasks;
using MarketPlace.DataLayerr.DTO.Account;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Areas.User.Controllers
{
    public class AccountController : UserBaseController
    {
        #region constructor



        #endregion

        #region user dashboard

        [HttpGet("change-password")]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [HttpGet("change-password") , ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO passwordDTO)
        {
            return View();
        }

        #endregion
    }
}
