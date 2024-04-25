// using backendtask.Model;
// using System.Collections.Generic;

// namespace backendtask.Repo
// {
//     public interface IDateAndInvoice
//     {
//         List<DateAndInvoice> GetAllDatesPeriods();
//     }
// }

using backendtask.Model;
using System.Collections.Generic;

namespace backendtask.Repositories
{
    public interface IDateAndInvoiceRepository
    {
        IEnumerable<SubmissionDateAndInvoicePeriod> GetSubmissionDatesAndPeriods();
    }
}
