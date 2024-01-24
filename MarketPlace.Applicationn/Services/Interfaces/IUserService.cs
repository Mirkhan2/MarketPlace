using System;
using System.Threading.Tasks;
using MarketPlace.DataLayerr.DTO.Account;
using MarketPlace.DataLayerr.Entities.Account;
using Microsoft.AspNetCore.Http;

namespace MarketPlace.Applicationn.Services.Interfaces
{
    public interface IUserService : IAsyncDisposable
    {
        #region account

        Task<RegisterUserResult> RegisterUser(RegisterUserDTO register);
        Task<bool> IsUserExistsByMobileNumber(string mobile);
        Task<LoginUserResult> GetUserForLogin(LoginUserDTO login);
        Task<User> GetUserByMobile(string mobile);
        Task<ForgotPasswordResult> RecoverUserPassword(ForgotPasswordDTO forget);
        Task<bool> ActivateMobile(ActivateMobileDTO activate);
        Task<bool> ChangeUserPassword(ChangePasswordDTO changePass, long currentUserId);
        Task<EditUserProfileDTO > GetProfileForEdit(long userId);
        Task<EditUserProfileResult> EditUserProfile(EditUserProfileDTO profile, long userId , IFormFile avatarImage);



        #endregion

    }
}