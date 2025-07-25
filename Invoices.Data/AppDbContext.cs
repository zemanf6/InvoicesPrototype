using Invoices.Data.Entities;
using Invoices.Data.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Data
{
    /// <summary>
    /// Hlavní třída pro práci s databází pomocí Entity Framework Core
    /// Obsahuje definice tabulek a konfigurace entit
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Konstruktor, který přijímá možnosti konfigurace databáze
        /// Tuto třídu zaregistrujeme v Program.cs pro Dependency Injection
        /// </summary>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Tabulka osob (pojištěnců).
        /// DbSet představuje kolekci entit v databázi
        /// </summary>
        public DbSet<Person> Persons { get; set; }

        /// <summary>
        /// Místo, kde se definují specifické konfigurace pro jednotlivé entity
        /// Volá se automaticky při vytváření modelu
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfigurace entity Person:
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

                // Enum Country se ukládá do databáze jako řetězec místo číselné hodnoty
                builder.Property(p => p.Country)
                       .HasConversion<string>();

                builder.HasIndex(p => p.IdentificationNumber);

                // Přidáme index na sloupec Hidden – zrychlí filtrování skrytých osob
                builder.HasIndex(p => p.Hidden);
            });
        }
    }
}
