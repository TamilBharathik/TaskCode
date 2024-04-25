using backendtask.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace backendtask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubmissionController : ControllerBase
    {
        private readonly IDateAndInvoiceRepository _dateAndInvoiceRepository;

        public SubmissionController(IDateAndInvoiceRepository dateAndInvoiceRepository)
        {
            _dateAndInvoiceRepository = dateAndInvoiceRepository ?? throw new ArgumentNullException(nameof(dateAndInvoiceRepository));
        }

        [HttpGet("submission-dates-and-periods")]
        public IActionResult GetSubmissionDatesAndPeriods()
        {
            try
            {
                var submissionDatesAndPeriods = _dateAndInvoiceRepository.GetSubmissionDatesAndPeriods();
                return Ok(submissionDatesAndPeriods);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving submission dates and invoice periods: " + ex.Message);
            }
        }
    }
}
