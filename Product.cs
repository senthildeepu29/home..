namespace ShopForHome.Api.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Category { get; set; } = null!;
        public int Stock { get; set; }

        public decimal Rating { get; set; }
        public string ImageUrl { get; set; } = null!;
    }
}