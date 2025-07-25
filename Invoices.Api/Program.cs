using Invoices.Data.Repositories.Interfaces;
using Invoices.Data.Repositories;
using System.Text.Json;
using Invoices.Api;
using Invoices.Api.Managers.Interfaces;
using Invoices.Api.Managers;
using Invoices.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Invoices.Api.Validators;
using FluentValidation;
using Invoices.Data.Entities.Enums;
using Invoices.Data.Entities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddValidatorsFromAssemblyContaining<PersonDtoValidator>();

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonManager, PersonManager>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceManager, InvoiceManager>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapperProfile>();
});

builder.Services.AddSwaggerGen(options =>
{
    options.SchemaFilter<DateOnlySchemaFilter>();
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseLazyLoadingProxies()
           .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.Persons.Any())
    {
        db.Persons.AddRange(
            new Person
            {
                Name = "Jan Nov�k",
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
                Note = "Testovac� osoba",
                Hidden = false
            },
            new Person
            {
                Name = "Anna Horv�thov�",
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
                Note = "Druh� osoba",
                Hidden = false
            }
        );

        db.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
