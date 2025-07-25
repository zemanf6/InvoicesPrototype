using Invoices.Data.Entities;
using Invoices.Data.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aby se enum Country v databázi ukládal jako string a ne jako číslo
            modelBuilder.Entity<Person>(builder =>
            {
                builder.HasData(
                        new Person
                        {
                            Id = 1,
                            Name = "Jan Novák",
                            IdentificationNumber = "12345678",
                            TaxNumber = "CZ12345678",
                            AccountNumber = "1234567890",
                            BankCode = "0100",
                            Iban = "CZ6501000000001234567890",
                            Telephone = "+420123456789",
                            Mail = "jan.novak@example.com",
                            Street = "Ulice 1",
                            Zip = "10000",
                            City = "Praha",
                            Country = Country.CZECHIA,
                            Note = "Testovací osoba",
                            Hidden = false
                        },
                        new Person
                        {
                            Id = 2,
                            Name = "Anna Horváthová",
                            IdentificationNumber = "87654321",
                            TaxNumber = "SK87654321",
                            AccountNumber = "0987654321",
                            BankCode = "0900",
                            Iban = "SK8909000000000987654321",
                            Telephone = "+421987654321",
                            Mail = "anna.horvathova@example.sk",
                            Street = "Cesta 5",
                            Zip = "81101",
                            City = "Bratislava",
                            Country = Country.SLOVAKIA,
                            Note = "Druhá osoba",
                            Hidden = false
                        }
                    );
                builder.Property(p => p.Country)
                .HasConversion<string>();

                builder.HasIndex(p => p.IdentificationNumber);

                builder.HasIndex(p => p.Hidden);
            });
                

            modelBuilder.Entity<Invoice>(builder =>
            {
                builder.HasOne(i => i.Buyer)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(i => i.BuyerId)
                    .OnDelete(DeleteBehavior.Restrict);

                builder.HasOne(i => i.Seller)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(i => i.SellerId)
                    .OnDelete(DeleteBehavior.Restrict);

                builder.Property(i => i.Price)
                    .HasColumnType("decimal(18,2)");

                builder.Property(i => i.Vat)
                    .HasColumnType("decimal(5, 2)");

                builder.HasIndex(i => i.BuyerId);
                builder.HasIndex(i => i.SellerId);
                builder.HasIndex(i => i.Product);
                builder.HasIndex(i => i.Price);
                builder.HasIndex(i => i.Issued);
            });
        }
    }
}
