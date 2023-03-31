namespace WebApiTienda.Helpers
{
    public class ProductRequest
    {
        public string? Search { get; set; }
        public string? Category { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? OrderField { get; set; }
        public bool Ascending { get; set; }

    }
}
