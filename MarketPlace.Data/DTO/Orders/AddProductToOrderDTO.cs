namespace MarketPlace.Data.DTO.Orders
{
    public class AddProductToOrderDTO
    {
        public long ProductId { get; set; }
        public long? ProductColorId { get; set; }
        public int Count { get; set; }
    }
}
