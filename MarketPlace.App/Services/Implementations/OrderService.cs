
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MarketPlace.App.Services.Interfaces;
using MarketPlace.Data.DTO.Orders;
using MarketPlace.Data.Entities.ProductOrder;
using MarketPlace.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MarketPlace.App.Services.Implementations
{
    public class OrderService : IOrderService
    {
        #region constructor

        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<OrderDetail> _orderDetailRepository;

        public OrderService(IGenericRepository<Order> orderRepository, IGenericRepository<OrderDetail> orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
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
                .SingleOrDefaultAsync(s => s.UserId == userId && !s.IsPaid);

            return userOpenOrder;
        }

        #endregion

        #region order detail


        public async Task AddProductToOpenOrder(long userId, AddProductToOrderDTO order)
        {
            var openOrder = await GetUserLatestOpenOrder(userId);

            var similarOrder = openOrder.OrderDetails.SingleOrDefault(s =>
                s.ProductId == order.ProductId && s.ProductColorId == order.ProductColorId);

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
                Details = userOpenOrder.OrderDetails.Select(s => new UserOpenOrderDetailItemDTO
                {
                    Count = s.Count,
                    ColorName = s.ProductColor?.ColorName,
                    ProductColorId = s.ProductColorId,
                    ProductColorPrice = s.ProductColor?.Price  ?? 0,
                    ProductPrice = s.Product.Price,
                    ProductTitle = s.Product.Title,
                  //  ProductIamgeName = s.Product.ImageName
                }).ToList()
            };
            return null;
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
