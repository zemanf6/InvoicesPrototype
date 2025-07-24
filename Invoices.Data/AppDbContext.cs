using Invoices.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aby se enum Country v databázi ukládal jako string a ne jako číslo
            modelBuilder.Entity<Person>()
                .Property(p => p.Country)
                .HasConversion<string>();

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
            });

            

        }
    }
}
