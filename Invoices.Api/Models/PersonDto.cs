using Invoices.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Invoices.Api.Models
{
    public class PersonDto
    {
        [JsonPropertyName("_id")]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string IdentificationNumber { get; set; } = "";

        [Required]
        public string TaxNumber { get; set; } = "";

        [Required]
        public string AccountNumber { get; set; } = "";

        [Required]
        public string BankCode { get; set; } = "";

        [Required]
        public string Iban { get; set; } = "";

        [Required]
        public string Telephone { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Mail { get; set; } = "";

        [Required]
        public string Street { get; set; } = "";

        [Required]
        public string Zip { get; set; } = "";

        [Required]
        public string City { get; set; } = "";

        [Required]
        public Country Country { get; set; }

        [Required]
        public string Note { get; set; } = "";
    }
}
