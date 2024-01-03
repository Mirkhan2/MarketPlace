using System.Linq;
using System.Threading.Tasks;
using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Account;
using MarketPlace.DataLayerr.Entities.Account;
using MarketPlace.DataLayerr.Repository;

namespace MarketPlace.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        #region constructor

        private readonly IGenericRepository<User> _userRepository;

        public UserService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion

        #region
        public async Task<RegisterUserResult> RegisterUser(RegisterUserDTO register)
        {
            if (!await IsUserExistsByMobileNumber(register.Mobile))
            {
                var user = new User
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Mobile = register.Mobile,
                    Password = _passwordHelper.EncodePasswordMd5(register.Password)
                };

                await _userRepository.AddEntity(user);
                await _userRepository.SaveChanges();
                // todo: send activation mobile code to user
                return RegisterUserResult.Success;
            }

            return RegisterUserResult.MobileExists;
        }

        public async Task<bool> IsUserExistsByMobileNumber(string mobile)
        {
            return await _userRepository.GetQuery().AsQueryable().AnyAsync(s => s.Mobile == mobile);
        }
        #endregion

        #region dispose

        public async ValueTask DisposeAsync()
        {
            await _userRepository.DisposeAsync();
        }

        public Task<bool> IsUserExistsByMobileNumber(string mobile)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
