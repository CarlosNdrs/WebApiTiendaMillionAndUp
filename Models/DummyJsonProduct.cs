namespace WebApiTienda.Models
{
    public class DummyJsonProduct
    {
        public DummyJsonProduct()
        {
            Source = "DummyJson";
        }
        public string Source { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal Rating { get; set; }
        public int Stock { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Thumbnail { get; set; }
        public string[] Images { get; set; }
    }
}
