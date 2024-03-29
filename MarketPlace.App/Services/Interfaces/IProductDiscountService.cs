using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketPlace.Data.DTO.Dicount;
using MarketPlace.Data.Entities.ProductDiscount;

namespace MarketPlace.App.Services.Interfaces
{
    public  interface IProductDiscountService : IAsyncDisposable 
    {

        #region product discount

        Task<FilterProductDiscountDTO> FilterProductDiscount(FilterProductDiscountDTO filter);

       Task<CreateDiscountResult> CreateProductDiscount(CreateProductDiscountDto discount, long sellerId);

        #endregion

    }
}
