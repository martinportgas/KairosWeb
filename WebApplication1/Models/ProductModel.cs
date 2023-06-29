namespace KairosWeb.Models
{
    public class ProductModel
    {
        public int? ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int ProductQty { get; set; }
        public DateTime ProductDate { get; set; }
    }
}
