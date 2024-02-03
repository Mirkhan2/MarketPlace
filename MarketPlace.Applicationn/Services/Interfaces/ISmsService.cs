using System.Threading.Tasks;

namespace MarketPlace.Applicationn.Services.Interfaces
{
	public interface ISmsService
	{
		Task SendVerificationSms(string mobile, string activationCode);
        Task SendUserPasswordSms(string mobile, string password);


    }
}
