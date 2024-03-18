using System.Threading.Tasks;
using MarketPlace.App.Services.Interfaces;
using MarketPlace.Data.DTO.Products;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Controllers
{
    public class ProductController : SiteBaseController
    {
        #region constructor

        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        #endregion

        #region filter products

        [HttpGet("products")]
        [HttpGet("product/{Category}")]
        public async Task<IActionResult> FilterProducts(FilterProductDTO filter)
        {
            filter.TakeEntity = 9;
            filter.FilterProductState = FilterProductState.Accepted;
            filter = await _productService.FilterProducts(filter);
            var products = await _productService.FilterProducts(filter);
            ViewBag.ProductsCategories = await _productService.GetAllActiveProductCategories();
            if (filter.PageId > filter.GetLastPage()&& filter.GetLastPage() != 0) return NotFound();
            return View(filter);
        }
        #endregion



        #region show product detail
        [HttpGet ("products/{productsId}/{title}")]
        public async Task<IActionResult> ProductDetail(long productId , string title)
        {
            var product = await _productService.GetProductDetailById(productId);
            if (product == null) return NotFound();
            return View(product);
        }

        #endregion
       

    }
}
