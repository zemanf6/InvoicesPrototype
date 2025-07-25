using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Invoices.Data.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "AccountNumber", "BankCode", "City", "Country", "Hidden", "Iban", "IdentificationNumber", "Mail", "Name", "Note", "Street", "TaxNumber", "Telephone", "Zip" },
                values: new object[,]
                {
                    { 1, "1234567890", "0100", "Praha", "CZECHIA", false, "CZ6501000000001234567890", "12345678", "jan.novak@example.com", "Jan Novák", "Testovací osoba", "Ulice 1", "CZ12345678", "+420123456789", "10000" },
                    { 2, "0987654321", "0900", "Bratislava", "SLOVAKIA", false, "SK8909000000000987654321", "87654321", "anna.horvathova@example.sk", "Anna Horváthová", "Druhá osoba", "Cesta 5", "SK87654321", "+421987654321", "81101" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
