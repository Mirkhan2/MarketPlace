using System.Threading.Tasks;
using MarketPlace.App.Services.Interfaces;

namespace MarketPlace.App.Services.Implementations
{
	public  class SmsService : ISmsService
	{
		private string apiKey = "کد ارسال پیام";

        public async Task SendVerificationSms(string mobile, string activationCode)
        {

            Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(apiKey);
            api.VerifyLookup(mobile, activationCode, "Verification");
       ;

        }

        public async Task SendUserPasswordSms(string mobile, string password)
        {

            Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(apiKey);
             api.VerifyLookup(mobile, password, "Verification");

        }

      
    }
}
