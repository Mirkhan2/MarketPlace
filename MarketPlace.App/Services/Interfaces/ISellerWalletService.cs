using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Data.DTO.SellerWallet;

namespace MarketPlace.App.Services.Interfaces
{
    public interface ISellerWalletService
    {
        #region wallet

        Task<FilterSellerWalletDTO> FilterSellerWalletDTO(FilterSellerWalletDTO filter);
        #endregion
    }
}
