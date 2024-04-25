using backendtask.Model;
using backendtask.Repo;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backendtask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddInvoiceController : ControllerBase
    {
       private readonly IAddInvoice repo;

        public AddInvoiceController(IAddInvoice repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] Addinvoice entry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await repo.Create(entry);

            return Ok(result);
        }
    }
}