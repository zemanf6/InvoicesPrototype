using Invoices.Data.Entities;
using Invoices.Data.Repositories.Interfaces;

namespace Invoices.Data.Repositories
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(AppDbContext context) : base(context) { }

        public IEnumerable<Invoice> GetAll(
        int? buyerId = null,
        int? sellerId = null,
        string? product = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        int? limit = null)
        {
            IQueryable<Invoice> query = _dbSet.AsQueryable();

            if (buyerId is not null)
                query = query.Where(i => i.BuyerId == buyerId);

            if (sellerId is not null)
                query = query.Where(i => i.SellerId == sellerId);

            if (product is not null)
                query = query.Where(i => i.Product == product);

            if (minPrice is not null)
                query = query.Where(i => i.Price >= minPrice);

            if (maxPrice is not null)
                query = query.Where(i => i.Price <= maxPrice);

            if (limit is int notNullLimit)
                query = query.Take(notNullLimit);

            return query.ToList();
        }
    }
}
