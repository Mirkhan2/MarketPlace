
using MarketPlace.Data.DTO.Orders;
using MarketPlace.Data.Entities.ProductOrder;

namespace MarketPlace.App.EntitiExtensions
{
    public static class OrderExtensions
    {
        public static string GetOrderDetailWithDiscountPrice(this UserOpenOrderDetailItemDTO detail)
        {

            if (detail.DiscountPercentage != null)
            {
                return (detail.ProductPrice * detail.DiscountPercentage.Value / 100 * detail.Count).ToString("#,0");

            }
            return "----";
        }
    }
}
