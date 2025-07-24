using Invoices.Data.Entities;
using Invoices.Data.Repositories.Interfaces;

namespace Invoices.Data.Repositories
{
    public class InvoiceRepository: Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(AppDbContext context) : base(context) { }
    }
}
