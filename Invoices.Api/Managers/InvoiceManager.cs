using AutoMapper;
using Invoices.Api.Managers.Interfaces;
using Invoices.Api.Models;
using Invoices.Data.Entities;
using Invoices.Data.Repositories.Interfaces;

namespace Invoices.Api.Managers
{
    public class InvoiceManager : IInvoiceManager
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public InvoiceManager(IInvoiceRepository invoiceRepository, IMapper mapper, IPersonRepository personRepository)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
            _personRepository = personRepository;
        }

        public IEnumerable<InvoiceDto> GetAll(InvoiceFilterDto? filter = null)
        {
            IEnumerable<Invoice> invoices = _invoiceRepository.GetAll(
                buyerId: filter?.BuyerId,
                sellerId: filter?.SellerId,
                product: filter?.Product,
                minPrice: filter?.MinPrice,
                maxPrice: filter?.MaxPrice,
                limit: filter?.Limit
            );

            return _mapper.Map<IEnumerable<InvoiceDto>>(invoices);
        }

        public InvoiceDto? GetById(int id)
        {
            Invoice? invoice = _invoiceRepository.GetById(id);
            if (invoice is null)
                return null;

            return _mapper.Map<InvoiceDto>(invoice);
        }

        public InvoiceDto? Create(InvoiceDto dto)
        {
            Invoice? invoice = TryMapInvoice(null, dto);
            if (invoice is null)
                return null;

            Invoice addedInvoice = _invoiceRepository.Add(invoice);
            _invoiceRepository.SaveChanges();

            return _mapper.Map<InvoiceDto>(addedInvoice);
        }

        public InvoiceDto? Edit(int id, InvoiceDto dto)
        {
            if (!_invoiceRepository.ExistsWithId(id))
                return null;

            Invoice? invoice = TryMapInvoice(id, dto);
            if (invoice is null)
                return null;

            Invoice updatedInvoice = _invoiceRepository.Update(invoice);
            _invoiceRepository.SaveChanges();

            return _mapper.Map<InvoiceDto>(updatedInvoice);
        }

        public bool Delete(int id)
        {
            Invoice? invoice = _invoiceRepository.GetById(id);
            if (invoice is null)
                return false;

            _invoiceRepository.Delete(invoice);
            _invoiceRepository.SaveChanges();
            return true;
        }

        private Invoice? TryMapInvoice(int? id, InvoiceDto dto)
        {
            if (dto.Buyer?.Id is not int buyerId || !_personRepository.ExistsWithId(buyerId))
                return null;

            if (dto.Seller?.Id is not int sellerId || !_personRepository.ExistsWithId(sellerId))
                return null;

            /* Pattern matching by byl na některé moc, když tak klasicky:
             * if (dto.Buyer?.Id == null)
                    return null;

               int buyerId = dto.Buyer.Id;

               if (!_personRepository.ExistsWithId(buyerId))
                    return null;
             *
             * A stejně pro sellera
             */

            Invoice invoice = _mapper.Map<Invoice>(dto);

            invoice.Id = id ?? default;
            invoice.BuyerId = buyerId;
            invoice.SellerId = sellerId;
            invoice.Buyer = null!;
            invoice.Seller = null!;

            return invoice;
        }

        public IList<InvoiceDto> GetSales(string identificationNumber)
        {
            IList<Person> persons = _personRepository.GetAllByIdentificationNumber(identificationNumber);

            List<Invoice> invoices = persons
                .SelectMany(p => p.Sales)
                .Distinct()
                .ToList();

            return _mapper.Map<IList<InvoiceDto>>(invoices);
        }

        public IList<InvoiceDto> GetPurchases(string identificationNumber)
        {
            IList<Person> persons = _personRepository.GetAllByIdentificationNumber(identificationNumber);

            List<Invoice> invoices = persons
                .SelectMany(p => p.Purchases)
                .Distinct()
                .ToList();

            return _mapper.Map<IList<InvoiceDto>>(invoices);
        }

        public InvoiceStatisticsDto GetStatistics()
        {
            IEnumerable<Invoice> allInvoices = _invoiceRepository.GetAll();

            decimal currentYearSum = allInvoices
                .Where(i => i.Issued.Year == DateTime.Now.Year)
                .Sum(i => i.Price);

            decimal allTimeSum = allInvoices.Sum(i => i.Price);
            int count = allInvoices.Count();

            return new InvoiceStatisticsDto
            {
                CurrentYearSum = currentYearSum,
                AllTimeSum = allTimeSum,
                InvoicesCount = count
            };
        }
    }
}
