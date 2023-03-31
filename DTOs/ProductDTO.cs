using WebApiTienda.Models;

namespace WebApiTienda.DTOs
{
    public class ProductDTO
    {
        public string Source { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPercentage { get; set; }
        public string Description { get; set; }
        public int? Stock { get; set; }
        public string? Brand { get; set; }
        public string Category { get; set; }
        public string Thumbnail { get; set; }
        public string[]? Images { get; set; }
        public RatingFakeStore Rating { get; set; }
    }
}
