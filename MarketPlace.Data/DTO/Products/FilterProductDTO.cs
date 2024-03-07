using System;
using System.Collections.Generic;
using MarketPlace.Data.DTO.Paging;
using MarketPlace.Data.Entities.Products;

namespace MarketPlace.Data.DTO.Products
{
    public class FilterProductDTO : BasePaging
    {
        #region properteis

        public string ProductTitle { get; set; }

        public long? SellerId { get; set; }

        public FilterProductState FilterProductState { get; set; }

       public List<Product> Products { get; set; }

        #endregion

        #region methods

        public FilterProductDTO SetProducts(List<Product> products)
        {
            this.Products = products;
            return this;
        }

        public FilterProductDTO SetPaging(BasePaging paging)
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

      

        #endregion
    }

    public enum FilterProductState
    {
        UnderProgress,
        Accepted,
        Rejected,
        Active,
        NotActive
    }
}
