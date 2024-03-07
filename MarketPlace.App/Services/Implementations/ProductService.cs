using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.App.Services.Interfaces;
using MarketPlace.Data.DTO.Paging;
using MarketPlace.Data.DTO.Products;
using MarketPlace.Data.Entities.Products;
using MarketPlace.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.App.Services.Implementations
{
    public class ProductService : IProductService
    {
        #region constructor
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductCategory> _productCategoryRepository;
        private readonly IGenericRepository<ProductSelectedCategory> _productSelectedCategoryRepository;

        public ProductService(IGenericRepository<Product> productRepository, IGenericRepository<ProductCategory> productCategoryRepository, IGenericRepository<ProductSelectedCategory> productSelectedCategoryRepository)

        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _productSelectedCategoryRepository = productSelectedCategoryRepository;
        }
        #endregion

        #region products

        public async Task<FilterProductDTO> FilterProducts(FilterProductDTO filter)
        {
            var query = _productRepository.GetQuery().AsQueryable();

            #region state
            switch (filter.FilterProductState)
            {
                case FilterProductState.Active:
                    query = query.Where(s => s.IsActive && s.ProductAcceptanceState == ProductAcceptanceState.Accepted);
                    break;
                case FilterProductState.NotActive:
                     query = query.Where(s => !s.IsActive && s.ProductAcceptanceState == ProductAcceptanceState.Accepted);
                    break;
                case FilterProductState.Accepted:
                    query = query.Where(s =>  s.ProductAcceptanceState == ProductAcceptanceState.Accepted);
                    break;
                case FilterProductState.Rejected:
                    query = query.Where(s => s.IsActive && s.ProductAcceptanceState == ProductAcceptanceState.Rejected);
                    break;
                case FilterProductState.UnderProgress:
                    query = query.Where(s => s.IsActive && s.ProductAcceptanceState == ProductAcceptanceState.UnderProgress);
                    break;
            }
            #endregion
            #region filter

            if (!string.IsNullOrEmpty(filter.ProductTitle))
                query = query.Where(s => EF.Functions.Like(s.Title, $"%{filter.ProductTitle}%"));

            if (filter.SellerId != null && filter.SellerId != 0)
                query = query.Where(s => s.SellerId == filter.SellerId.Value);


            #endregion
           
            #region paging

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();

            #endregion


            return filter.SetProducts(allEntities).SetPaging(pager);
        }

        #endregion



        #region dispose

        public async ValueTask DisposeAsync()
        {
            await _productCategoryRepository.DisposeAsync();
            await _productSelectedCategoryRepository.DisposeAsync();

            await _productRepository.DisposeAsync();
        }



        #endregion

    }
}

