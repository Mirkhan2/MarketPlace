using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Data.DTO.Products;

namespace MarketPlace.App.Services.Interfaces
{
    public interface IProductService : IAsyncDisposable
    {
        #region products

        Task<FilterProductDTO> FilterProducts(FilterProductDTO filter);

        #endregion
    }
}
