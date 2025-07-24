using Invoices.Api.Managers.Interfaces;
using Invoices.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers
{
    [ApiController]
    [Route("api/identification")]
    public class IdentificationController : ControllerBase
    {
        private readonly IInvoiceManager _invoiceManager;

        public IdentificationController(IInvoiceManager invoiceManager)
        {
            _invoiceManager = invoiceManager;
        }

        [HttpGet("{identificationNumber}/sales")]
        public ActionResult<IList<InvoiceDto>> GetSales(string identificationNumber)
        {
            var invoices = _invoiceManager.GetSales(identificationNumber);
            return Ok(invoices);
        }

        [HttpGet("{identificationNumber}/purchases")]
        public ActionResult<IList<InvoiceDto>> GetPurchases(string identificationNumber)
        {
            var invoices = _invoiceManager.GetPurchases(identificationNumber);
            return Ok(invoices);
        }
    }
}
