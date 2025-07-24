using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoices.Data.Migrations
{
    /// <inheritdoc />
    public partial class Indexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdentificationNumber",
                table: "Persons",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Product",
                table: "Invoices",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Hidden",
                table: "Persons",
                column: "Hidden");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_IdentificationNumber",
                table: "Persons",
                column: "IdentificationNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Issued",
                table: "Invoices",
                column: "Issued");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Price",
                table: "Invoices",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Product",
                table: "Invoices",
                column: "Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Persons_Hidden",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_IdentificationNumber",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_Issued",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_Price",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_Product",
                table: "Invoices");

            migrationBuilder.AlterColumn<string>(
                name: "IdentificationNumber",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Product",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
