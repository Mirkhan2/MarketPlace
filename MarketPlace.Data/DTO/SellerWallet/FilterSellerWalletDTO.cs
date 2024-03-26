﻿using System.Collections.Generic;
using MarketPlace.Data.DTO.Paging;

namespace MarketPlace.Data.DTO.SellerWallet
{
    public class FilterSellerWalletDTO : BasePaging
    {
        public long? SellerId { get; set; }

        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
        public List<Entities.Wallet.SellerWallet> MyProperty { get; set; }
        public FilterSellerWalletDTO SetSellerWallets (List<Entities.Wallet.SellerWallet> wallets)
        { 
            SellerWallets = wallets;

                return this;
        }

            public FilterSellerWalletDTO SetPaging(BasePaging paging )
        {

            this.PageId = paging.PageId;
            this.AllEntitiesCount = paging.AllEntitiesCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
            this.TakeEntity = paging.TakeEntity;
            this.SkipEntity = paging.SkipEntity;
            this.PageCount = paging.PageCount;

            return this;
        }
    }
}
