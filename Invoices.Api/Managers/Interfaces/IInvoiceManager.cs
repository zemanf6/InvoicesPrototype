using Invoices.Api.Models;

namespace Invoices.Api.Managers.Interfaces
{
    public interface IInvoiceManager
    {
        IEnumerable<InvoiceDto> GetAll(InvoiceFilterDto? filter = null);
        InvoiceDto? GetById(int id);
        InvoiceDto? Create(InvoiceDto dto);
        InvoiceDto? Edit(int id, InvoiceDto dto);
        bool Delete(int id);
        IList<InvoiceDto> GetPurchases(string identificationNumber);
        IList<InvoiceDto> GetSales(string identificationNumber);
        InvoiceStatisticsDto GetStatistics();
    }
}
