namespace WebApiTienda.Helpers
{
    public class PurchaseRequest
    {
        public int Total { get; set; }
        public PurchaseDetailRequest[] Products { get; set; }
    }
}
