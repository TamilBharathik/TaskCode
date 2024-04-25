// using System;

// namespace backendtask.Model
// {
//     public class DateAndInvoice
//     {
//         public DateTime SubmissionDate { get; set; }
//         public string? InvoicePeriod { get; set; }
//     }
// }
using System;

namespace backendtask.Model
{
    public class SubmissionDateAndInvoicePeriod
    {
        public DateTime SubmissionDate { get; set; }
        public string? CompareInvoicePeriod { get; set; }
    }
}
