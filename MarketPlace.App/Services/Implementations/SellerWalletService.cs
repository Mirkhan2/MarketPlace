using System.Linq;
using System.Threading.Tasks;
using MarketPlace.App.Services.Interfaces;
using MarketPlace.Data.DTO.Paging;
using MarketPlace.Data.DTO.SellerWallet;
using MarketPlace.Data.Entities.Wallet;
using MarketPlace.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.App.Services.Implementations
{
    public class SellerWalletService : ISellerWalletService
    {
        #region constructor
        private readonly IGenericRepository<SellerWallet> _sellerWalletRepository;
        public SellerWalletService(IGenericRepository<SellerWallet> sellerWalletRepository)
        {
            _sellerWalletRepository = sellerWalletRepository;

        }


        #endregion

        #region wallet

        public async Task<FilterSellerWalletDTO> FilterSellerWalletDTO(FilterSellerWalletDTO filter)
        {
            var query = _sellerWalletRepository.GetQuery().AsQueryable();

            if (filter.SellerId != null && filter.SellerId != 0)
            {
                query = query.Where(s => s.SellerId == filter.SellerId.Value);
            }

            if (filter.PriceFrom != null)
            {
                query = query.Where(s => s.Price > filter.PriceFrom.Value);

            }

            if (filter.PriceFrom != null)
            {
                query = query.Where(s => s.Price < filter.PriceFrom.Value);

            }
            var AllEntitiesCount = await query.CountAsync();

            var pager = Pager.Build(filter.PageId, AllEntitiesCount, filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);

            var wallets = await query.Paging(pager).ToListAsync();

            return filter.SetSellerWallets(wallets).SetPaging(pager);
        }
        public async Task AddWallet(SellerWallet wallet)
        {
            _sellerWalletRepository.AddEntity(wallet);
            await _sellerWalletRepository.SaveChanges();
        }

        #endregion


    }
}
