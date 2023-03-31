namespace WebApiTienda.Helpers
{
    public class ResponseRequest
    {
        public bool Success { get; set; }
        public int OrderId { get; set; }
        public string? MessageError { get; set; }
    }
}
