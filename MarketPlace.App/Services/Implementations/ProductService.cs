﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MarketPlace.App.Extensions;
using MarketPlace.App.Services.Interfaces;
using MarketPlace.App.Utils;
using MarketPlace.Data.DTO.Common;
using MarketPlace.Data.DTO.Paging;
using MarketPlace.Data.DTO.Products;
using MarketPlace.Data.Entities.Products;
using MarketPlace.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static MarketPlace.Data.DTO.Products.EditProductDTO;

namespace MarketPlace.App.Services.Implementations
{
    public class ProductService : IProductService
    {

        #region constructor

        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductCategory> _productCategoryRepository;
        private readonly IGenericRepository<ProductSelectedCategory> _productSelectedCategoryRepository;
        private readonly IGenericRepository<ProductColor> _productColorRepository;
        private readonly IGenericRepository<ProductGallery> _productGalleryRepository;
        private readonly IGenericRepository<ProductFeature> _productFeatureRepository;
        private readonly IGenericRepository<ProductDiscount> _productDiscountRepository;

        public ProductService(IGenericRepository<Product> productRepository, IGenericRepository<ProductCategory> productCategoryRepository, IGenericRepository<ProductSelectedCategory> productSelectedCategoryRepository, IGenericRepository<ProductColor> productColorRepository, IGenericRepository<ProductGallery> productGalleryRepository, IGenericRepository<ProductFeature> productFeatureRepository, IGenericRepository<ProductDiscount> productDiscountRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _productSelectedCategoryRepository = productSelectedCategoryRepository;
            _productColorRepository = productColorRepository;
            _productGalleryRepository = productGalleryRepository;
            _productFeatureRepository = productFeatureRepository;
            _productDiscountRepository = productDiscountRepository;
        }

        #endregion

        #region products

        public async Task<CreateProductResult> CreateProduct(CreateProductDTO product, long sellerId, IFormFile productImage)
        {
            if (productImage == null) return CreateProductResult.HasNoImage;

            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(productImage.FileName);

            var res = productImage.AddImageToServer(imageName, PathExtension.ProductImageServer, 150, 150, PathExtension.ProductThumbnailImageImageServer);

            if (res)
            {
                // create product
                var newProduct = new Product
                {
                    Title = product.Title,
                    Price = product.Price,
                    ShortDescription = product.ShortDescription,
                    Description = product.Description,
                    IsActive = product.IsActive,
                    SellerId = sellerId,
                    ImageName = imageName,
                    ProductAcceptanceState = ProductAcceptanceState.UnderProgress
                };

                await _productRepository.AddEntity(newProduct);
                await _productRepository.SaveChanges();

                await AddProductSelectedCategories(newProduct.Id, product.SelectedCategories);
                await AddProductSelectedColors(newProduct.Id, product.ProductColors);
                await _productSelectedCategoryRepository.SaveChanges();

                return CreateProductResult.Success;
            }

            return CreateProductResult.Error;
        }

        public async Task<bool> AcceptSellerProduct(long productId)
        {
            var product = await _productRepository.GetEntityById(productId);
            if (product != null)
            {
                product.ProductAcceptanceState = ProductAcceptanceState.Accepted;
                product.ProductAcceptOrRejectDescription = $"محصول مورد نظر در تاریخ {DateTime.Now.ToShamsi()} مورد تایید سایت قرار گرفت";
                _productRepository.EditEntity(product);
                await _productRepository.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<bool> RejectSellerProduct(RejectItemDTO reject)
        {
            var product = await _productRepository.GetEntityById(reject.Id);
            if (product != null)
            {
                product.ProductAcceptanceState = ProductAcceptanceState.Rejected;
                product.ProductAcceptOrRejectDescription = reject.RejectedMessage;
                _productRepository.EditEntity(product);
                await _productRepository.SaveChanges();

                return true;
            }

            return false;
        }

        public async Task<EditProductDTO> GetProductForEdit(long productId)
        {
            var product = await _productRepository.GetEntityById(productId);
            if (product == null) return null;

            return new EditProductDTO
            {
                Id = productId,
                Description = product.Description,
                ShortDescription = product.ShortDescription,
                Price = product.Price,
                IsActive = product.IsActive,
                ImageName = product.ImageName,
                Title = product.Title,
                ProductColors = await _productColorRepository
                    .GetQuery().AsQueryable()
                    .Where(s => !s.IsDelete && s.ProductId == productId)
                    .Select(s => new CreateProductColorDTO { Price = s.Price, ColorName = s.ColorName, ColorCode = s.ColorCode }).ToListAsync(),
                SelectedCategories = await _productSelectedCategoryRepository.GetQuery().AsQueryable()
                    .Where(s => s.ProductId == productId).Select(s => s.ProductCategoryId).ToListAsync(),
                ProductFeatures = await _productFeatureRepository.GetQuery().AsQueryable()
                    .Where(s => !s.IsDelete && s.ProductId == productId)
                    .Select(f => new CreateProductFeatureDTO
                    {
                        FeatureValue = f.FeatureValue,
                        Feature = f.FeatureTitle
                    }).ToListAsync()
            };
        }

        public async Task<EditProductResult> EditSellerProduct(EditProductDTO product, long userId, IFormFile productImage)
        {
            var mainProduct = await _productRepository.GetQuery().AsQueryable()
                .Include(s => s.Seller)
                .SingleOrDefaultAsync(s => s.Id == product.Id);
            if (mainProduct == null) return EditProductResult.NotFound;
            if (mainProduct.Seller.UserId != userId) return EditProductResult.NotForUser;

            mainProduct.Title = product.Title;
            mainProduct.ShortDescription = product.ShortDescription;
            mainProduct.Description = product.Description;
            mainProduct.IsActive = product.IsActive;
            mainProduct.Price = product.Price;
            mainProduct.ProductAcceptanceState = ProductAcceptanceState.UnderProgress;

            if (productImage != null)
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(productImage.FileName);

                var res = productImage.AddImageToServer(imageName, PathExtension.ProductImageServer, 150, 150,
                    PathExtension.ProductThumbnailImageImageServer, mainProduct.ImageName);

                if (res)
                {
                    mainProduct.ImageName = imageName;
                }
            }

            await RemoveAllProductSelectedCategories(product.Id);
            await AddProductSelectedCategories(product.Id, product.SelectedCategories);
            await _productSelectedCategoryRepository.SaveChanges();
            await RemoveAllProductSelectedColors(product.Id);
            await AddProductSelectedColors(product.Id, product.ProductColors);
            await RemoveAllProductFeatures(product.Id);
            await CreateProductFeatures(product.Id, product.ProductFeatures);
            await _productColorRepository.SaveChanges();

            return EditProductResult.Success;
        }

        public async Task RemoveAllProductSelectedCategories(long productId)
        {
            _productSelectedCategoryRepository.DeletePermanentEntities(await _productSelectedCategoryRepository.GetQuery().AsQueryable().Where(s => s.ProductId == productId).ToListAsync());
        }

        public async Task RemoveAllProductSelectedColors(long productId)
        {
            _productColorRepository.DeletePermanentEntities(await _productColorRepository.GetQuery().AsQueryable().Where(s => s.ProductId == productId).ToListAsync());
        }

        public async Task AddProductSelectedColors(long productId, List<CreateProductColorDTO> colors)
        {
            if (colors != null && colors.Any())
            {
                var productSelectedColors = new List<ProductColor>();

                foreach (var productColor in colors)
                {
                    if (productSelectedColors.All(s => s.ColorName != productColor.ColorName))
                    {
                        productSelectedColors.Add(new ProductColor
                        {
                            ColorName = productColor.ColorName,
                            Price = productColor.Price,
                            ProductId = productId,
                            ColorCode = productColor.ColorCode
                        });
                    }
                }

                await _productColorRepository.AddRangeEntities(productSelectedColors);
            }
        }

        public async Task AddProductSelectedCategories(long productId, List<long> selectedCategories)
        {
            if (selectedCategories != null && selectedCategories.Any())
            {
                var productSelectedCategories = new List<ProductSelectedCategory>();

                foreach (var categoryId in selectedCategories)
                {
                    productSelectedCategories.Add(new ProductSelectedCategory
                    {
                        ProductCategoryId = categoryId,
                        ProductId = productId
                    });
                }
                await _productSelectedCategoryRepository.AddRangeEntities(productSelectedCategories);
            }
        }

        public async Task<List<Product>> FilterProductsForSellerByProductName(long sellerId, string productName)
        {
            return await _productRepository.GetQuery()
                .AsQueryable()
                .Where(s =>
                    s.SellerId == sellerId &&
                    EF.Functions.Like(s.Title, $"%{productName}%")).ToListAsync();
        }
        public async Task<List<ProductDiscount>> GetAllOffProducts(int take)
        {
            return await _productDiscountRepository.GetQuery().AsQueryable()
                .Include(s => s.Product)
                .Where(s => s.ExpireDate >= DateTime.Now)
                    .OrderByDescending(s => s.ExpireDate)
                    .Skip(0)
                    .Take(take)

                .Distinct()
                .ToListAsync();
        }



        public async Task<FilterProductDTO> FilterProducts(FilterProductDTO filter)
        {
            var query = _productRepository.GetQuery()
                .Include(s => s.ProductSelectedCategories)
                .ThenInclude(s => s.ProductCategory)
                .AsQueryable();

            var expensiveProduct = await query.OrderByDescending(s => s.Price).FirstOrDefaultAsync();
            filter.FilterMaxPrice = expensiveProduct.Price;

            #region state

            switch (filter.FilterProductState)
            {
                case FilterProductState.All:
                    break;
                case FilterProductState.Active:
                    query = query.Where(s => s.IsActive && s.ProductAcceptanceState == ProductAcceptanceState.Accepted);
                    break;
                case FilterProductState.NotActive:
                    query = query.Where(s => !s.IsActive && s.ProductAcceptanceState == ProductAcceptanceState.Accepted);
                    break;
                case FilterProductState.Accepted:
                    query = query.Where(s => s.IsActive && s.ProductAcceptanceState == ProductAcceptanceState.Accepted);
                    break;
                case FilterProductState.Rejected:
                    query = query.Where(s => s.ProductAcceptanceState == ProductAcceptanceState.Rejected);
                    break;
                case FilterProductState.UnderProgress:
                    query = query.Where(s => s.ProductAcceptanceState == ProductAcceptanceState.UnderProgress);
                    break;
            }

            switch (filter.OrderBy)
            {
                case FilterProductOrderBy.CreateDate_Des:
                    query = query.OrderByDescending(s => s.CreateDate);
                    break;
                case FilterProductOrderBy.CreateDate_Acs:
                    query = query.OrderBy(s => s.CreateDate);
                    break;
                case FilterProductOrderBy.Price_Des:
                    query = query.OrderByDescending(s => s.Price);
                    break;
                case FilterProductOrderBy.Price_Acs:
                    query = query.OrderBy(s => s.Price);
                    break;
            }

            #endregion

            #region filter

            if (!string.IsNullOrEmpty(filter.ProductTitle))
                query = query.Where(s => EF.Functions.Like(s.Title, $"%{filter.ProductTitle}%"));

            if (filter.SellerId != null && filter.SellerId != 0)
                query = query.Where(s => s.SellerId == filter.SellerId.Value);

            if (filter.SelectedMaxPrice == 0) filter.SelectedMaxPrice = expensiveProduct.Price;

            query = query.Where(s => s.Price >= filter.SelectedMinPrice);
            query = query.Where(s => s.Price <= filter.SelectedMaxPrice);

            if (!string.IsNullOrEmpty(filter.Category))
                query = query.Where(s =>
                    s.ProductSelectedCategories.Any(f => f.ProductCategory.UrlName == filter.Category));

            #endregion

            #region paging

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();

            #endregion

            return filter.SetProducts(allEntities).SetPaging(pager);
        }


        #endregion

        #region product gallery

        public async Task<List<ProductGallery>> GetAllProductGalleries(long productId)
        {
            return await _productGalleryRepository.GetQuery().AsQueryable()
                .Where(s => s.ProductId == productId).ToListAsync();
        }

        public async Task<Product> GetProductBySellerOwnerId(long productId, long userId)
        {
            return await _productRepository.GetQuery()
                .Include(s => s.Seller)
                .SingleOrDefaultAsync(s => s.Id == productId && s.Seller.UserId == userId);
        }

        public async Task<List<ProductGallery>> GetAllProductGalleriesInSellerPanel(long productId, long sellerId)
        {
            return await _productGalleryRepository.GetQuery()
                .Include(s => s.Product)
                .Where(s => s.ProductId == productId && s.Product.SellerId == sellerId).ToListAsync();
        }

        public async Task<CreateOrEditProductGalleryResult> CreateProductGallery(CreateOrEditProductGalleryDTO gallery, long productId, long sellerId)
        {
            var product = await _productRepository.GetEntityById(productId);
            if (product == null) return CreateOrEditProductGalleryResult.ProductNotFound;
            if (product.SellerId != sellerId) return CreateOrEditProductGalleryResult.NotForUserProduct;
            if (gallery.Image == null || !gallery.Image.IsImage()) return CreateOrEditProductGalleryResult.ImageIsNull;

            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(gallery.Image.FileName);
            gallery.Image.AddImageToServer(imageName, PathExtension.ProductGalleryImageServer, 100, 100,
                PathExtension.ProductGalleryThumbnailImageServer);

            await _productGalleryRepository.AddEntity(new ProductGallery
            {
                ProductId = productId,
                ImageName = imageName,
                DisplayPriority = gallery.DisplayPriority
            });

            await _productGalleryRepository.SaveChanges();

            return CreateOrEditProductGalleryResult.Success;
        }

        public async Task<CreateOrEditProductGalleryDTO> GetProductGalleryForEdit(long galleryId, long sellerId)
        {
            var gallery = await _productGalleryRepository.GetQuery()
                .Include(s => s.Product)
                .SingleOrDefaultAsync(s => s.Id == galleryId && s.Product.SellerId == sellerId);

            if (gallery == null) return null;

            return new CreateOrEditProductGalleryDTO
            {
                ImageName = gallery.ImageName,
                DisplayPriority = gallery.DisplayPriority
            };
        }

        public async Task<CreateOrEditProductGalleryResult> EditProductGallery(long galleryId, long sellerId, CreateOrEditProductGalleryDTO gallery)
        {
            var mainGallery = await _productGalleryRepository.GetQuery()
                .Include(s => s.Product)
                .SingleOrDefaultAsync(s => s.Id == galleryId);

            if (mainGallery == null) return CreateOrEditProductGalleryResult.ProductNotFound;

            if (mainGallery.Product.SellerId != sellerId) return CreateOrEditProductGalleryResult.NotForUserProduct;

            if (gallery.Image != null && gallery.Image.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(gallery.Image.FileName);
                var result = gallery.Image.AddImageToServer(imageName, PathExtension.ProductGalleryImageServer, 100, 100,
                     PathExtension.ProductGalleryThumbnailImageServer, mainGallery.ImageName);
                mainGallery.ImageName = imageName;
            }

            mainGallery.DisplayPriority = gallery.DisplayPriority;
            _productGalleryRepository.EditEntity(mainGallery);
            await _productGalleryRepository.SaveChanges();
            return CreateOrEditProductGalleryResult.Success;
        }

        public async Task<ProductDetailDTO> GetProductDetailById(long productId)
        {
            var product = await _productRepository.GetQuery()
                .AsQueryable()
                .Include(s => s.Seller)
                .ThenInclude(s => s.User)
                .Include(s => s.ProductSelectedCategories)
                .ThenInclude(s => s.ProductCategory)
                .Include(s => s.ProductGalleries)
                .Include(s => s.ProductColors)
                .Include(s => s.ProductFeatures)
                .SingleOrDefaultAsync(s => s.Id == productId);

            if (product == null) return null;

            var selectedCategoriesIds = product.ProductSelectedCategories.Select(f => f.ProductCategoryId).ToList();

            return new ProductDetailDTO
            {
                ProductId = productId,
                Price = product.Price,
                ImageName = product.ImageName,
                Description = product.Description,
                ShortDescription = product.ShortDescription,
                Seller = product.Seller,
                ProductCategories = product.ProductSelectedCategories.Select(s => s.ProductCategory).ToList(),
                ProductGalleries = product.ProductGalleries.ToList(),
                Title = product.Title,
                ProductColors = product.ProductColors.ToList(),
                SellerId = product.SellerId,
                ProductFeatures = product.ProductFeatures.ToList(),
                RelatedProducts = await _productRepository.GetQuery()
                    .Include(s => s.ProductSelectedCategories)
                    .Where(s => s.ProductSelectedCategories.Any(f => selectedCategoriesIds.Contains(f.ProductCategoryId)) && s.Id != productId && s.ProductAcceptanceState == ProductAcceptanceState.Accepted)
                    .ToListAsync()
            };
        }

        #endregion

        #region product categories

        public async Task<List<ProductCategory>> GetAllProductCategoriesByParentId(long? parentId)
        {
            if (parentId == null || parentId == 0)
            {
                return await _productCategoryRepository.GetQuery()
                    .AsQueryable()
                    .Where(s => !s.IsDelete && s.IsActive && s.ParentId == null)
                    .ToListAsync();
            }

            return await _productCategoryRepository.GetQuery()
                .AsQueryable()
                .Where(s => !s.IsDelete && s.IsActive && s.ParentId == parentId)
                .ToListAsync();
        }

        public async Task<List<ProductCategory>> GetAllActiveProductCategories()
        {
            return await _productCategoryRepository.GetQuery().AsQueryable()
                .Where(s => s.IsActive && !s.IsDelete).ToListAsync();
        }



        #endregion

        #region product feature

        public async Task CreateProductFeatures(long productId, List<CreateProductFeatureDTO> features)
        {
            var newFeatures = new List<ProductFeature>();
            if (features != null && features.Any())
            {
                foreach (var feature in features)
                {
                    newFeatures.Add(new ProductFeature()
                    {
                        ProductId = productId,
                        FeatureTitle = feature.Feature,
                        FeatureValue = feature.FeatureValue
                    });
                }

                await _productFeatureRepository.AddRangeEntities(newFeatures);
                await _productFeatureRepository.SaveChanges();
            }
        }

        public async Task RemoveAllProductFeatures(long productId)
        {
            var productFeatures = await _productFeatureRepository.GetQuery()
                .Where(s => s.ProductId == productId)
                .ToListAsync();

            _productFeatureRepository.DeletePermanentEntities(productFeatures);
            await _productFeatureRepository.SaveChanges();
        }

        #endregion

        #region dispose

        public async ValueTask DisposeAsync()
        {
            await _productCategoryRepository.DisposeAsync();
            await _productRepository.DisposeAsync();
            await _productSelectedCategoryRepository.DisposeAsync();
            await _productFeatureRepository.DisposeAsync();
            await _productSelectedCategoryRepository.DisposeAsync();
            await _productColorRepository.DisposeAsync();
        }


        #endregion
    }
}

