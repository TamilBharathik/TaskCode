using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using backendtask.Model;
using System.Data;
using backendtask.Repo;
using backendtask.Repositories;
using Dapper.Oracle;
using System.Reflection.Metadata;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly string? _connectionString;

    public InvoiceRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");
    }

    public void CreateInvoice(Invoice entry)
{
    try
    {
        using (var connection = new OracleConnection(_connectionString))
        {
            connection.Open();

            // Generate a new GUID
            Guid invoiceNo = Guid.NewGuid();

            // Convert the GUID to a byte array
            byte[] invoiceNoBytes = invoiceNo.ToByteArray();

            // Add the byte array as a parameter
            var parameters = new OracleDynamicParameters();
            parameters.Add("invoiceNo_in", invoiceNoBytes);
            parameters.Add("invoicePeriod_in", entry.invoicePeriod);
            parameters.Add("departmentID_in", entry.DepartmentID);
            parameters.Add("departmentName_in", entry.DepartmentName);
            parameters.Add("totalUSDAmt_in", entry.TotalUSDAmount);
            parameters.Add("submissionDate_in", entry.SubmissionDate);

            connection.Execute("TB_PACKAGE.insert_invoice", parameters, commandType: CommandType.StoredProcedure);
        }
    }
    catch (Exception ex)
    {
        // Log or handle the exception appropriately
        throw new Exception($"An error occurred while inserting the invoice: {ex.Message}", ex);
    }
}


}
