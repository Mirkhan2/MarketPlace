using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Data.DTO.Orders;
using MarketPlace.Data.Entities.ProductOrder;

namespace MarketPlace.App.Services.Interfaces
{
    public interface IOrderService  : IAsyncDisposable
    {
        #region order

        Task<long> AddOrderForUser(long userId);
        Task<Order> GetUserLatestOpenOrder(long userId);
        Task<int> GetTotalOrderPriceForPayment(long userId);
        Task PayOrderProductPriceToSeller(long userId , long refId);

        #endregion

        #region order detail

        Task ChangeOrderDetailCount(long detailId , long userId , int count);
        
        Task AddProductToOpenOrder(long userId, AddProductToOrderDTO order);

        Task<UserOpenOrderDTO> GetUserOpenOrderDetail(long userId);
        Task<bool> RemoveOrderDetail(long detilId , long sellerId);

        #endregion

    }
}
