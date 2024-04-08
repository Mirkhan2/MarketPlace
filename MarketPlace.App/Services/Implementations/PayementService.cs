using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.App.Services.Interfaces;
using MarketPlace.Data.DTO.Common;

namespace MarketPlace.App.Services.Implementations
{
    public class PayementService : IPayementService
    {
        //amadesazi karbar be dargah
        public PayementStatus CreatePayementRequest(string merchantId, int amoount, string description, string callbackUrl, ref string redirectUrl, string userEmail, string userMobile)
        {
           var payement  = new ZarinpalSandbox.Payement(amount);
            var res = payement.payementRequest(description, callbackUrl, userEmail,userMobile);

            if (res.Result.Status == (int)PayementStatus.st100)
            {
                redirectUrl = "https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority;
                return (PaymentStatus) res.Result.Status;

            }
            return (PaymentStatus) res.Result.Status;


        }
    }
}
