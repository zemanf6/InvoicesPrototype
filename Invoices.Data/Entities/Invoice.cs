namespace Invoices.Data.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public int InvoiceNumber { get; set; }
        public DateOnly Issued { get; set; }
        public DateOnly DueDate { get; set; }
        public required string Product { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public required string Note { get; set; }

        public int BuyerId { get; set; }
        public int SellerId { get; set; }

        public virtual Person Buyer { get; set; } = null!;
        public virtual Person Seller { get; set; } = null!;
    }
}
