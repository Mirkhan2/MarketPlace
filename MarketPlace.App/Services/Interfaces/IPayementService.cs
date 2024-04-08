using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Data.DTO.Common;

namespace MarketPlace.App.Services.Interfaces
{
    public  interface IPayementService
    {
        PayementStatus CreatePayementRequest(string merchantId , int amoount , string description,
            string callbackUrl , ref string redirectUrl , string userEmail , string userMobile);
    }
}
