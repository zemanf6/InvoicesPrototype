using Invoices.Api.Managers.Interfaces;
using Invoices.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceManager _invoiceManager;

        public InvoicesController(IInvoiceManager invoiceManager)
        {
            _invoiceManager = invoiceManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InvoiceDto>> GetAll([FromQuery] InvoiceFilterDto? filter)
        {
            var invoices = _invoiceManager.GetAll(filter);
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public ActionResult<InvoiceDto> GetById(int id)
        {
            InvoiceDto? invoice = _invoiceManager.GetById(id);
            if (invoice is null)
                return NotFound();

            return Ok(invoice);
        }

        [HttpPost]
        public ActionResult<InvoiceDto> Create([FromBody] InvoiceDto dto)
        {
            InvoiceDto? createdInvoice = _invoiceManager.Create(dto);

            if (createdInvoice is null)
                return BadRequest("Invalid buyer or seller id");

            return CreatedAtAction(nameof(GetById), new { id = createdInvoice.Id }, createdInvoice);
        }

        [HttpPut("{id}")]
        public ActionResult<InvoiceDto> Edit(int id, [FromBody] InvoiceDto dto)
        {
            InvoiceDto? updatedInvoice = _invoiceManager.Edit(id, dto);
            if (updatedInvoice is null)
                return BadRequest();

            return Ok(updatedInvoice);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool deleted = _invoiceManager.Delete(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("statistics")]
        public ActionResult<InvoiceStatisticsDto> GetStatistics()
        {
            var result = _invoiceManager.GetStatistics();
            return Ok(result);
        }
    }
}
