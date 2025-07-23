using Invoices.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aby se enum Country v databázi ukládal jako string a ne jako číslo
            modelBuilder.Entity<Person>()
            .Property(p => p.Country)
            .HasConversion<string>();
        }
    }
}
