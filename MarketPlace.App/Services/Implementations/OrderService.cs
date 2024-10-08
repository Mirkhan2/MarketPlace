﻿using System;
using System.Linq;
using System.Threading.Tasks;
using MarketPlace.App.Services.Interfaces;
using MarketPlace.Data.DTO.Orders;
using MarketPlace.Data.Entities.ProductOrder;
using MarketPlace.Data.Entities.Products;
using MarketPlace.Data.Entities.Wallet;
using MarketPlace.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.App.Services.Implementations
{
    public class OrderService : IOrderService
    {
        #region constructor

        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<OrderDetail> _orderDetailRepository;
        private readonly ISellerWalletService _sellerWalletService;
        private readonly IGenericRepository<ProductDiscount> _productDiscountRepository;
        private readonly IGenericRepository<ProductDiscountUse> _productDiscountUseRepository;

        public OrderService(IGenericRepository<Order> orderRepository, IGenericRepository<OrderDetail> orderDetailRepository, ISellerWalletService sellerWalletService, IGenericRepository<ProductDiscount> productDiscountRepository, IGenericRepository<ProductDiscountUse> productDiscountUseRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _sellerWalletService = sellerWalletService;
            _productDiscountRepository = productDiscountRepository;
            _productDiscountUseRepository = productDiscountUseRepository;
        }

        #endregion

        #region order

        public async Task<long> AddOrderForUser(long userId)
        {
            var order = new Order { UserId = userId };

            await _orderRepository.AddEntity(order);

            await _orderRepository.SaveChanges();

            return order.Id;
        }

        public async Task<Order> GetUserLatestOpenOrder(long userId)
        {
            if (!await _orderRepository.GetQuery().AnyAsync(s => s.UserId == userId && !s.IsPaid))
                await AddOrderForUser(userId);

            var userOpenOrder = await _orderRepository.GetQuery()
                .Include(s => s.OrderDetails)
                .ThenInclude(s => s.ProductColor)
                .Include(s => s.OrderDetails)
                .ThenInclude(s => s.Product)
                .ThenInclude(s => s.ProductDiscounts)
                .SingleOrDefaultAsync(s => s.UserId == userId && !s.IsPaid);

            return userOpenOrder;
        }

        public async Task<int> GetTotalOrderPriceForPayment(long userId)
        {
            var userOpenOrder = await GetUserLatestOpenOrder(userId);
            int totalPrice = 0;
            int discount = 0;


            foreach (var detail in userOpenOrder.OrderDetails)
            {
                var oneProductPrice = detail.ProductColor != null
                    ? detail.Product.Price + detail.ProductColor.Price
                    : detail.Product.Price;

                var productDiscount = await _productDiscountRepository.GetQuery()
                    .Include(s => s.ProductDiscountUses)
                    .OrderByDescending(s => s.CreateDate)
                    .FirstOrDefaultAsync(s =>
                        s.ProductId == detail.ProductId && s.DiscountNumber - s.ProductDiscountUses.Count > 0);

                if (productDiscount != null)
                {
                    discount = (int)Math.Ceiling(oneProductPrice * productDiscount.Percentage / (decimal)100);
                }

                totalPrice += detail.Count * (oneProductPrice - discount);
            }

            return totalPrice;
        }

        public async Task PayOrderProductPriceToSeller(long userId, long refId)
        {
            var openOrder = await GetUserLatestOpenOrder(userId);

            foreach (var detail in openOrder.OrderDetails)
            {
                var productPrice = detail.Product.Price;
                var productColorPrice = detail.ProductColor?.Price ?? 0;
                var discount = 0;
                var totalPrice = detail.Count * (productPrice + productColorPrice);
                var productDiscount = await _productDiscountRepository.GetQuery()
                    .Include(s => s.ProductDiscountUses)
                    .OrderByDescending(s => s.CreateDate)
                    .FirstOrDefaultAsync(s =>
                        s.ProductId == detail.ProductId && s.DiscountNumber - s.ProductDiscountUses.Count > 0);

                if (productDiscount != null)
                {
                    discount = (int)Math.Ceiling(totalPrice * productDiscount.Percentage / (decimal)100);

                    var newDiscountUse = new ProductDiscountUse
                    {
                        UserId = userId,
                        ProductDiscountId = productDiscount.Id,
                    };

                    await _productDiscountUseRepository.AddEntity(newDiscountUse);
                }

                var totalPriceWithDiscount = totalPrice - discount;

                await _sellerWalletService.AddWallet(new SellerWallet
                {
                    SellerId = detail.Product.SellerId,
                    Price = (int)Math.Ceiling(totalPriceWithDiscount * (100 - detail.Product.SiteProfit) / (double)100),
                    TransactionType = TransactionType.Deposit,
                    Description = $"پرداخت مبلغ {totalPriceWithDiscount} تومان جهت فروش {detail.Product.Title} به تعداد {detail.Count} عدد با سهم تهیین شده ی {100 - detail.Product.SiteProfit} درصد"
                });

                detail.ProductPrice = totalPriceWithDiscount;
                detail.ProductColorPrice = productColorPrice;
                _orderDetailRepository.EditEntity(detail);
            }

            openOrder.IsPaid = true;
            openOrder.TracingCode = refId.ToString();
            openOrder.PaymentDate = DateTime.Now;
            _orderRepository.EditEntity(openOrder);
            await _orderRepository.SaveChanges();
        }

        public async Task ChangeOrderDetailCount(long detailId, long userId, int count)
        {
            var userOpenOrder = await GetUserLatestOpenOrder(userId);
            var detail = userOpenOrder.OrderDetails.SingleOrDefault(s => s.Id == detailId);
            if (detail != null)
            {
                if (count > 0)
                {
                    detail.Count = count;
                }
                else
                {
                    _orderDetailRepository.DeleteEntity(detail);
                }
                await _orderDetailRepository.SaveChanges();
            }
        }

        #endregion

        #region order detail

        public async Task AddProductToOpenOrder(long userId, AddProductToOrderDTO order)
        {
            var openOrder = await GetUserLatestOpenOrder(userId);

            var similarOrder = openOrder.OrderDetails.SingleOrDefault(s =>
                s.ProductId == order.ProductId && s.ProductColorId == order.ProductColorId && !s.IsDelete);

            if (similarOrder == null)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = openOrder.Id,
                    ProductId = order.ProductId,
                    ProductColorId = order.ProductColorId,
                    Count = order.Count
                };

                await _orderDetailRepository.AddEntity(orderDetail);
                await _orderDetailRepository.SaveChanges();
            }
            else
            {
                similarOrder.Count += order.Count;
                await _orderDetailRepository.SaveChanges();
            }
        }

        public async Task<UserOpenOrderDTO> GetUserOpenOrderDetail(long userId)
        {
            var userOpenOrder = await GetUserLatestOpenOrder(userId);

            return new UserOpenOrderDTO
            {
                UserId = userId,
                Description = userOpenOrder.Description,
                Details = userOpenOrder.OrderDetails
                    .Where(s => !s.IsDelete)
                    .Select(s => new UserOpenOrderDetailItemDTO
                    {
                        DetailId = s.Id,
                        Count = s.Count,
                        ColorName = s.ProductColor?.ColorName,
                        ProductColorId = s.ProductColorId,
                        ProductColorPrice = s.ProductColor?.Price ?? 0,
                        ProductId = s.ProductId,
                        ProductPrice = s.Product.Price,
                        ProductTitle = s.Product.Title,
                        ProductImageName = s.Product.ImageName,
                        DiscountPercentage = s.Product.ProductDiscounts
                        .OrderByDescending(a => a.CreateDate)
                        .FirstOrDefault(a => a.ExpireDate > DateTime.Now)?.Percentage
                    }).ToList()
            };
        }

        public async Task<bool> RemoveOrderDetail(long detailId, long userId)
        {
            var openOrder = await GetUserLatestOpenOrder(userId);
            var orderDetail = openOrder.OrderDetails.SingleOrDefault(s => s.Id == detailId);
            if (orderDetail == null) return false;

            _orderDetailRepository.DeleteEntity(orderDetail);
            await _orderDetailRepository.SaveChanges();

            return true;
        }

        #endregion

        #region dispose

        public async ValueTask DisposeAsync()
        {
            await _orderRepository.DisposeAsync();
            await _orderDetailRepository.DisposeAsync();
        }



        #endregion
    }
}
