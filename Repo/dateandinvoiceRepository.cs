using backendtask.Context;
using backendtask.Model;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace backendtask.Repositories
{
    public class DateAndInvoiceRepository : IDateAndInvoiceRepository
    {
        private readonly DapperDBContext _dbContext;

        public DateAndInvoiceRepository(DapperDBContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IEnumerable<SubmissionDateAndInvoicePeriod> GetSubmissionDatesAndPeriods()
        {
            using (var connection = _dbContext.CreateConnection())
            {
                connection.Open();
                var submissionDates = connection.Query<DateTime>("SELECT submission_date FROM invoice_TB");
                var invoicePeriods = connection.Query<string>("SELECT invoice_period FROM invoice_TB");

                var result = new List<SubmissionDateAndInvoicePeriod>();
                // Assuming both submissionDates and invoicePeriods have the same count
                for (int i = 0; i < submissionDates.Count(); i++)
                {
                    result.Add(new SubmissionDateAndInvoicePeriod
                    {
                        SubmissionDate = submissionDates.ElementAt(i),
                        CompareInvoicePeriod = invoicePeriods.ElementAt(i)
                    });
                }
                return result;
            }
        }
    }
}

