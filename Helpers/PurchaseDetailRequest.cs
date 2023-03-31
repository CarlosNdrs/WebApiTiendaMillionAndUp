namespace WebApiTienda.Helpers
{
    public class PurchaseDetailRequest
    {
        public string Store { get; set; }
        public int IdProduct { get; set; }
        public string Title { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalProduct { get; set; }
    }
}
