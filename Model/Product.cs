using Microsoft.AspNetCore.Http;

namespace backendtask.Model
{
    public class Product
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? Country { get; set; }
        public string? InvoicePeriod { get; set; }
        public string? ScrapType { get; set; }
        public int ManCost { get; set; }
        public int MaterialCost { get; set; }
        public int EstimateCost { get; set; }
        public int LocalAmount { get; set; }
        public IFormFile Image { get; set; }
        public string ImageFileName { get; set; }
    }
}
