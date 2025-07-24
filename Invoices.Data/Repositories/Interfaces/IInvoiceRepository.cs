using Invoices.Data.Entities;

namespace Invoices.Data.Repositories.Interfaces
{
    public interface IInvoiceRepository: IRepository<Invoice>
    {
        IEnumerable<Invoice> GetAll(
        int? buyerId = null,
        int? sellerId = null,
        string? product = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        int? limit = null);
    }
}
