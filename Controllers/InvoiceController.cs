using backendtask.Repositories;
using Microsoft.AspNetCore.Mvc;
using backendtask.Model;

namespace backendtask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        [HttpPost]
        public IActionResult CreateInvoice([FromBody] Invoice invoice)
        {
            try
            {
                _invoiceRepository.CreateInvoice(invoice);
                return Ok("Invoice created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error creating invoice: " + ex.Message);
            }
        }
        
    }
}
