﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.App.Services.Interfaces;
using MarketPlace.Data.DTO.Paging;
using MarketPlace.Data.DTO.Seller;
using MarketPlace.Data.Entities.Account;
using MarketPlace.Data.Entities.Store;
using MarketPlace.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.App.Services.Implementations
{
    public class SellerService : ISellerService
    {
        #region constructor
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Seller> _sellerRepository;
        public SellerService(IGenericRepository<Seller> sellerRepository, IGenericRepository<User> userRepository)
        {
            _sellerRepository = sellerRepository;
            _userRepository = userRepository;


        }

        public async Task<RequestSellerResult> AddNewSellerRequest(RequestSellerDTO seller, long userId)
        {
            var user = await _userRepository.GetEntityById(userId);

            if (user.IsBlocked) return RequestSellerResult.HasNotPermission;

            var hasUnderProgressRequest = await _sellerRepository.GetQuery().AsQueryable().AnyAsync(s =>
            s.UserId == userId && s.StoreAcceptanceState == StoreAcceptanceState.UnderProgress);

            if (hasUnderProgressRequest) return RequestSellerResult.HasUnderProgressRequest;

            var newSeller = new Seller
            {
                UserId = userId,
                StoreName = seller.StoreName,
                Address = seller.Address,
                Phone = seller.Phone,
                StoreAcceptanceState = StoreAcceptanceState.UnderProgress
            };
            await _sellerRepository.AddEntity(newSeller);
            await _sellerRepository.SaveChanges();

            return RequestSellerResult.Success;
        }

        public async Task<FilterSellerDTO> FilterSellers(FilterSellerDTO filter)
        {
            var query = _sellerRepository.GetQuery()
                .Include(s => s.User)
                .AsQueryable();

            #region state

            switch (filter.State)
            {
                case FilterSellerState.All:
                    query = query.Where(s => !s.IsDelete);
                    break;
                case FilterSellerState.Accepted:
                    query = query.Where(s => s.StoreAcceptanceState == StoreAcceptanceState.Accepted && !s.IsDelete);
                    break;

                case FilterSellerState.UnderProgress:
                    query = query.Where(s => s.StoreAcceptanceState == StoreAcceptanceState.UnderProgress && !s.IsDelete);
                    break;
                case FilterSellerState.Rejected:
                    query = query.Where(s => s.StoreAcceptanceState == StoreAcceptanceState.Rejected && !s.IsDelete);
                    break;
            }

            #endregion

            #region filter

            if (filter.UserId != null && filter.UserId != 0)
                query = query.Where(s => s.UserId == filter.UserId);

            if (!string.IsNullOrEmpty(filter.StoreName))
                query = query.Where(s => EF.Functions.Like(s.StoreName, $"%{filter.StoreName}%"));

            if (!string.IsNullOrEmpty(filter.Phone))
                query = query.Where(s => EF.Functions.Like(s.Phone, $"%{filter.Phone}%"));

            if (!string.IsNullOrEmpty(filter.Mobile))
                query = query.Where(s => EF.Functions.Like(s.Mobile, $"%{filter.Mobile}%"));

            if (!string.IsNullOrEmpty(filter.Address))
                query = query.Where(s => EF.Functions.Like(s.Address, $"%{filter.Address}%"));

            #endregion

            #region paging

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();

            #endregion
            return filter;
        }
        public async Task<EditRequestSellerDTO> GetRequestSellerForEdit(long id , long currentUserId)
        {
            var seller = await _sellerRepository.GetEntityById(id);
            if(seller == null || seller.UserId != currentUserId) return null;
            return new EditRequestSellerDTO
            {
                Id = seller.Id,
                Phone = seller.Phone,   
                Address = seller.Address,
                StoreName = seller.StoreName
            };
        }
        public async Task<EditRequestSellerResult> EditRequestSeller(EditRequestSellerDTO request, long currentUserId)
        {
           var seller = await _sellerRepository.GetEntityById(request.Id);
            if (seller == null || seller.UserId != currentUserId) return EditRequestSellerResult.NotFound;

            seller.Phone = request.Phone;
            seller.Address = request.Address;
            seller.StoreName = request.StoreName;
            //kheyli mohem
            seller.StoreAcceptanceState = StoreAcceptanceState.UnderProgress;
            _sellerRepository.EditEntity(seller);
            await _sellerRepository.SaveChanges();

            return EditRequestSellerResult.Success;
        }


        #endregion
        #region dispose
        public async ValueTask DisposeAsync()
        {
            await _sellerRepository.DisposeAsync();
        }

       




        #endregion

    }
}
