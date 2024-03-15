using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Data.DTO.Common;
using MarketPlace.Data.DTO.Products;
using MarketPlace.Data.Entities.Products;
using Microsoft.AspNetCore.Http;
using static MarketPlace.Data.DTO.Products.EditProductDTO;

namespace MarketPlace.App.Services.Interfaces
{
    public interface IProductService : IAsyncDisposable
    {
        #region products

        Task<FilterProductDTO> FilterProducts(FilterProductDTO filter);
        Task<CreateProductResult> CreateProduct(CreateProductDTO product, long sellerId, IFormFile productImage);
        Task<bool> AcceptSellerProduct(long productId);
        Task<bool> RejectSellerProduct(RejectItemDTO reject);
        Task<EditProductDTO> GetProductForEdit(long productId);
        Task<EditProductResult> EditSellerProduct(EditProductDTO product, long userId, IFormFile productImage);
        Task RemoveAllProductSelectedCategories(long productId);
        Task RemoveAllProductSelectedColors(long productId);
        Task AddProductSelectedColors(long productId, List<CreateProductColorDTO> colors);
        Task AddProductSelectedCategories(long productId, List<long> selectedCategories);

		#endregion

		#region product gallery


		Task<List<ProductGallery>> GetAllProductGalleries(long productId);
		Task<Product> GetProductBySellerOwnerId(long productId, long userId);
		Task<List<ProductGallery>> GetAllProductGalleriesInSellerPanel(long productId, long sellerId);
		Task<CreateOrEditProductGalleryResult> CreateProductGallery(CreateOrEditProductGalleryDTO gallery, long productId, long sellerId);
		Task<CreateOrEditProductGalleryDTO> GetProductGalleryForEdit(long galleryId, long sellerId);

		Task<CreateOrEditProductGalleryResult> EditProductGallery(long galleryId, long sellerId,
			CreateOrEditProductGalleryDTO gallery);

		#endregion

		#region product categories

		Task<List<ProductCategory>> GetAllProductCategoriesByParentId(long? parentId);
        Task<List<ProductCategory>> GetAllActiveProductCategories();

        #endregion
    }
}
