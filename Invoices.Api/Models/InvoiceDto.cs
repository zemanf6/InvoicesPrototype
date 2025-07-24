using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Invoices.Api.Models
{
    public class InvoiceDto
    {
        [JsonPropertyName("_id")]
        public int Id { get; set; }

        [Required]
        public int InvoiceNumber { get; set; }

        [Required]
        public DateTime Issued { get; set; }

        [Required]
        public DateTime DuteDate { get; set; }

        [Required]
        public string Product { get; set; } = "";

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal Vat { get; set; }

        [Required]
        public string Note { get; set; } = "";

        [Required]
        public PersonDto Buyer { get; set; } = new();

        [Required]
        public PersonDto Seller { get; set; } = new();
    }
}
