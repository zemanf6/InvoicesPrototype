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
