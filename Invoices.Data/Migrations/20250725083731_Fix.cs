using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoices.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Persons_IdentificationNumber",
                table: "Persons");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_IdentificationNumber",
                table: "Persons",
                column: "IdentificationNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Persons_IdentificationNumber",
                table: "Persons");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_IdentificationNumber",
                table: "Persons",
                column: "IdentificationNumber",
                unique: true);
        }
    }
}
