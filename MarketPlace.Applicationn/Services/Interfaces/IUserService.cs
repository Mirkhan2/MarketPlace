using System;
using System.Threading.Tasks;
using MarketPlace.DataLayerr.DTOs.Account;

namespace MarketPlace.Application.Services.Interfaces
{
    public interface IUserService : IAsyncDisposable
    {
        #region account

        Task<RegisterUserResult> RegisterUser(RegisterUserDTO register);
        Task<bool> IsUserExistsByMobileNumber(string mobile);



        #endregion

    }
}