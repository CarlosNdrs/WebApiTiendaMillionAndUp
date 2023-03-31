using Microsoft.AspNetCore.Server.Kestrel.Core.Features;

namespace WebApiTienda.Models
{
    public class FakeStoreProduct
    {
        public FakeStoreProduct()
        {
            Source = "FakeStore";
        }

        public string Source { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public string Image { get; set; }
        public RatingFakeStore Rating { get; set; }

    }
}
