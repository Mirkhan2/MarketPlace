using System.Threading.Tasks;
using MarketPlace.Data.DTO.SellerWallet;
using MarketPlace.Data.Entities.Wallet;

namespace MarketPlace.App.Services.Interfaces
{
    public interface ISellerWalletService
    {
        #region wallet

        Task<FilterSellerWalletDTO> FilterSellerWalletDTO(FilterSellerWalletDTO filter);
        Task AddWallet(SellerWallet wallet);
        #endregion
    }
}
