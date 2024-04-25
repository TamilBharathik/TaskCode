using backendtask.Model;
using Dapper;
using Dapper.Oracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Threading.Tasks;

namespace backendtask.Repo
{
    public class AddInvoiceRepository : IAddInvoice
    {
        private readonly string _connectionString;

        public AddInvoiceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Addinvoice> Create(Addinvoice entry)
        {
            try
            {
                using (var con = new OracleConnection(_connectionString))
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("invoiceNo_in", entry.InvoiceNo);
                    parameters.Add("invoicePeriod_in", entry.invoicePeriod);
                    parameters.Add("departmentID_in", entry.DepartmentID);
                    parameters.Add("departmentName_in", entry.DepartmentName);
                    parameters.Add("totalUSDAmt_in", entry.TotalUSDAmount);

                    await con.ExecuteAsync("TB_PACKAGE.insert_invoice", parameters, commandType: CommandType.StoredProcedure);
                }
                return entry; // Return the input entry after successful insertion
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception($"An error occurred while inserting the invoice: {ex.Message}", ex);
            }
        }
    }
}
