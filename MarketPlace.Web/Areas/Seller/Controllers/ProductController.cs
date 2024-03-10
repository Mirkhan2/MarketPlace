using System.Threading.Tasks;
using MarketPlace.App.Extensions;
using MarketPlace.App.Services.Interfaces;
using MarketPlace.Data.DTO.Products;
using MarketPlace.Web.Http;
using MarketPlace.Web.PresentationExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Areas.Seller.Controllers
{
 
    public class ProductController : SellerBaseController
    {
        #region constructor

        private readonly IProductService _productService;
        private readonly ISellerService _sellerService;
        public ProductController(IProductService productService, ISellerService sellerService)
        {
            _productService = productService;
            _sellerService = sellerService;

        }
        #endregion

        #region list

        [HttpGet("products-list")]
        public async Task<IActionResult> Index(FilterProductDTO filter)
        {
            var seller = await _sellerService.GetLastActiveSellerByUserId(User.GetUserId());
            filter.SellerId = seller.Id;
            filter.FilterProductState = FilterProductState.All;
            return View(await _productService.FilterProducts(filter));
    
        }
        #endregion

        #region create product
        [HttpGet("create-product")]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.MainCategories = await _productService.GetallActiveProductCategories();

            return View();
        }
        [HttpPost("create-product"), ValidateAntiForgeryToken]

        public async Task<IActionResult> CreateProduct(CreateProductDTO product ,IFormFile productImage)
        {
            if (ModelState.IsValid)
            {
                var seller = await _sellerService.GetLastActiveSellerByUserId(User.GetUserId());
                var res = await _productService.CreateProduct(product,seller.Id,productImage);

                switch (res)
                {
                    case CreateProductResult.HasNoImage:
                        TempData[WarningMessage] = "aks";
                        break;
                    case CreateProductResult.Error:
                        TempData[ErrorMessage] = "Eror";
                        break;
                    case CreateProductResult.Success:
                        TempData[SuccessMessage] = "success{}";
                        return RedirectToAction("Index");
                        
                   
                }
            }
            ViewBag.MainCategories = await _productService.GetallActiveProductCategories();

            return View();   
        }
        #endregion

        #region product categories
        [HttpGet("product-categories/{parentId}")]
        public async Task<IActionResult> GetProductCategoriesByParent(long parentId)
        {
            var categories = await _productService.GetAllProductCategoriesByParentId(parentId); 

            return JsonResponseStatus.SendStatus(JsonResponsStatusType.Success, "INfomauoin" , categories);
        }
        #endregion
    }
}
