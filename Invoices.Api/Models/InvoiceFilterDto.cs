namespace Invoices.Api.Models
{
    public class InvoiceFilterDto
    {
        public int? BuyerId { get; set; }
        public int? SellerId { get; set; }
        public string? Product { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? Limit { get; set; }
    }
}
