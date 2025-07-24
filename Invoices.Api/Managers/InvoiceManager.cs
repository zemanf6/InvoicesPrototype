using AutoMapper;
using Invoices.Api.Managers.Interfaces;
using Invoices.Api.Models;
using Invoices.Data.Entities;
using Invoices.Data.Repositories.Interfaces;

namespace Invoices.Api.Managers
{
    public class InvoiceManager: IInvoiceManager
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

        public IEnumerable<InvoiceDto> GetAll()
        {
            var invoices = _invoiceRepository.GetAll();
            return _mapper.Map<IEnumerable<InvoiceDto>>(invoices);
        }

        public InvoiceDto? GetById(int id)
        {
            var invoice = _invoiceRepository.GetById(id);
            if (invoice is null)
                return null;

            return _mapper.Map<InvoiceDto>(invoice);
        }

        public InvoiceDto? Create(InvoiceDto dto)
        {
            if (dto.Buyer?.Id is not int buyerId || dto.Seller?.Id is not int sellerId)
                return null;

            Invoice invoice = _mapper.Map<Invoice>(dto);
            invoice.Id = default;
            invoice.BuyerId = buyerId;
            invoice.SellerId = sellerId;
            invoice.Buyer = null!;
            invoice.Seller = null!;

            Invoice addedInvoice = _invoiceRepository.Add(invoice);
            _invoiceRepository.SaveChanges();

            MapPersons(addedInvoice);

            return _mapper.Map<InvoiceDto>(addedInvoice);
        }

        public InvoiceDto? Edit(int id, InvoiceDto dto)
        {
            if (!_invoiceRepository.ExistsWithId(id))
                return null;

            if (dto.Buyer?.Id is not int buyerId || dto.Seller?.Id is not int sellerId)
                return null;

            Invoice invoice = _mapper.Map<Invoice>(dto);
            invoice.Id = id;
            invoice.BuyerId = buyerId;
            invoice.SellerId = sellerId;
            invoice.Buyer = null!;
            invoice.Seller = null!;

            Invoice updatedInvoice = _invoiceRepository.Update(invoice);
            _invoiceRepository.SaveChanges();

            MapPersons(updatedInvoice);

            return _mapper.Map<InvoiceDto>(updatedInvoice);
        }

        public bool Delete(int id)
        {
            var invoice = _invoiceRepository.GetById(id);
            if (invoice is null)
                return false;

            _invoiceRepository.Delete(invoice);
            _invoiceRepository.SaveChanges();
            return true;
        }

        private void MapPersons(Invoice invoice)
        {
            invoice.Buyer = _personRepository.GetById(invoice.BuyerId)!;
            invoice.Seller = _personRepository.GetById(invoice.SellerId)!;
        }
    }
}
