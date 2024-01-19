using System;
using System.Linq;
using System.Threading.Tasks;
using MarketPlace.Applicationn.Services.Interfaces;
using MarketPlace.DataLayerr.DTO.Account;
using MarketPlace.DataLayerr.Entities.Account;
using MarketPlace.DataLayerr.Repository;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Applicationn.Services.Implementations
{
    public class UserService : IUserService
    {
        #region constructor

        private readonly IGenericRepository<User> _userRepository;
        private readonly IPasswordHelper _passwordHelper;
		private readonly ISmsService _smsService;

		public UserService(IGenericRepository<User> userRepository, IPasswordHelper passwordHelper, ISmsService smsService)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
			_smsService = smsService;
		}

        #endregion

        #region account

        public async Task<RegisterUserResult> RegisterUser(RegisterUserDTO register)
        {
            if (!await IsUserExistsByMobileNumber(register.Mobile))
            {
                var user = new User
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Mobile = register.Mobile,
                    Password = _passwordHelper.EncodePasswordMd5(register.Password),
                    MobileActiveCode = new Random().Next(10000, 999999).ToString(),
                    EmailActiveCode = Guid.NewGuid().ToString("N")
                    
                };

                await _userRepository.AddEntity(user);
                await _userRepository.SaveChanges();
                await _smsService.SendVerificationSms(user.Mobile, user.MobileActiveCode );
                return RegisterUserResult.Success;
            }

            return RegisterUserResult.MobileExists;
        }

        public async Task<bool> IsUserExistsByMobileNumber(string mobile)
        {
            return await _userRepository.GetQuery().AsQueryable().AnyAsync(s => s.Mobile == mobile);
        }
        public async Task<LoginUserResult> GetUserForLogin(LoginUserDTO login)
        {
            var user = await _userRepository.GetQuery().AsQueryable().SingleOrDefaultAsync(s => s.Mobile == login.Mobile);
            if (user == null) return LoginUserResult.NotFound;
            if (!user.IsMobileActive) return LoginUserResult.NotActivated;
            if (user.Password != _passwordHelper.EncodePasswordMd5(login.Password)) return LoginUserResult.NotFound;
            return LoginUserResult.Success;
        }

        public async Task<User> GetUserByMobile(string mobile)
        {
            return await _userRepository.GetQuery().AsQueryable().SingleOrDefaultAsync(s => s.Mobile == mobile);
        }
        public async Task<ForgotPasswordResult> RecoverUserPassword(ForgotPasswordDTO forgot)
        {

            var user = await _userRepository.GetQuery().SingleOrDefaultAsync(s => s.Mobile == forgot.Mobile);
            if (user == null) return ForgotPasswordResult.NotFound;
            var newPassword = new Random().Next(1000000, 999999999).ToString();
            user.Password = _passwordHelper.EncodePasswordMd5(newPassword);
            _userRepository.EditEntity(user);
            // todo: send new password to user with sms
            await _userRepository.SaveChanges();

            return ForgotPasswordResult.Success;
        }
		public async Task<bool> ActivateMobile(ActivateMobileDTO activate)
		{
			var user = await _userRepository.GetQuery().SingleOrDefaultAsync(s => s.Mobile == activate.Mobile); 
            if (user != null)
            {
                user.IsEmailActive = true;
                user.MobileActiveCode = new Random().Next(1000000, 999999999).ToString();
                await _userRepository.SaveChanges();
				return true;
			}
			return false;
		}

		#endregion

		#region dispose

		public async ValueTask DisposeAsync()
        {
            await _userRepository.DisposeAsync();
        }

		


		#endregion
	}
}
