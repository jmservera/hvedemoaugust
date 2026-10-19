namespace ZavaStorefront.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int AvailableQuantity { get; set; }

        public bool IsOutOfStock => AvailableQuantity == 0;

        public string StockDescription => AvailableQuantity switch
        {
            0 => "Out of stock",
            <= 5 => $"{AvailableQuantity} left in stock",
            _ => "In stock"
        };
    }
}
