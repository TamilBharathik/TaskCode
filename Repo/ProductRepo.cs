using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using backendtask.Context;
using backendtask.Model;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using Dapper.Oracle;

namespace backendtask.Repo
{
    public class ProductRepo : IProductRepo
    {
        private readonly DapperDBContext context;

        public ProductRepo(DapperDBContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

public async Task<Product> Create(Product entry)
{
    try
    {
        if (entry.Image != null && entry.Image.Length > 0)
        {
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(entry.Image.FileName)}";
            string filePath = Path.Combine("C:\\AImages", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await entry.Image.CopyToAsync(stream);
            }

            // Store the filename in the 'Image' property
            entry.ImageFileName = fileName;
            entry.Image = null; // Ensure 'Image' property is null
        }

        // Establish connection to the database
        using (var connection = context.CreateConnection())
        {
            // Open the connection synchronously
            connection.Open();

            // Prepare parameters for the stored procedure
            var parameters = new OracleDynamicParameters();
            parameters.Add("productID_in", entry.ProductID);
            parameters.Add("productName_in", entry.ProductName);
            parameters.Add("country_in", entry.Country);
            parameters.Add("invoicePeriod_in", entry.InvoicePeriod);
            parameters.Add("scrapType_in", entry.ScrapType);
            parameters.Add("manCost_in", entry.ManCost);
            parameters.Add("materialCost_in", entry.MaterialCost);
            parameters.Add("estimateCost_in", entry.EstimateCost);
            parameters.Add("localAmount_in", entry.LocalAmount);
            parameters.Add("image_in", entry.ImageFileName);

            // Call the stored procedure
            await connection.ExecuteAsync("TB_PACKAGE.insert_record", parameters, commandType: CommandType.StoredProcedure);
        }

        // Return the created product
        return entry;
    }
    catch (Exception ex)
    {
        // Log or handle the exception appropriately
        throw new Exception($"An error occurred while creating the product: {ex.Message}");
    }
}



public async Task<List<Product>> GetAll()
{
    try
    {
        var products = new List<Product>();

        // Establish a connection to the Oracle database
        using (var connection = context.CreateConnection() as OracleConnection)
        {
            // Open the database connection asynchronously
            await connection.OpenAsync();

            // Create a command to execute the get_all_products_with_images procedure
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "TB_PACKAGE.get_all_products_with_images";
                command.CommandType = CommandType.StoredProcedure;

                // Add the output parameter for the cursor
                command.Parameters.Add("products_out", OracleDbType.RefCursor, ParameterDirection.Output);

                // Execute the stored procedure
                using (var reader = await command.ExecuteReaderAsync())
                {
                    // Read the result set and populate the list of products
                    while (await reader.ReadAsync())
                    {
                        var product = new Product
                        {
                            ProductID = reader.IsDBNull(reader.GetOrdinal("productID")) ? 0 : reader.GetInt32(reader.GetOrdinal("productID")),
                            ProductName = reader.IsDBNull(reader.GetOrdinal("productName")) ? string.Empty : reader.GetString(reader.GetOrdinal("productName")),
                            Country = reader.IsDBNull(reader.GetOrdinal("country")) ? string.Empty : reader.GetString(reader.GetOrdinal("country")),
                            InvoicePeriod = reader.IsDBNull(reader.GetOrdinal("invoicePeriod")) ? string.Empty : reader.GetString(reader.GetOrdinal("invoicePeriod")),
                            ScrapType = reader.IsDBNull(reader.GetOrdinal("scrapType")) ? string.Empty : reader.GetString(reader.GetOrdinal("scrapType")),
                            ManCost = reader.IsDBNull(reader.GetOrdinal("manCost")) ? 0 : reader.GetInt32(reader.GetOrdinal("manCost")),
                            MaterialCost = reader.IsDBNull(reader.GetOrdinal("materialCost")) ? 0 : reader.GetInt32(reader.GetOrdinal("materialCost")),
                            EstimateCost = reader.IsDBNull(reader.GetOrdinal("estimateCost")) ? 0 : reader.GetInt32(reader.GetOrdinal("estimateCost")),
                            LocalAmount = reader.IsDBNull(reader.GetOrdinal("localAmount")) ? 0 : reader.GetInt32(reader.GetOrdinal("localAmount")),
                            ImageFileName = reader.IsDBNull(reader.GetOrdinal("Image")) ? string.Empty : reader.GetString(reader.GetOrdinal("Image")) // Assigning Image column value to ImageFileName property
                        };

                        products.Add(product);
                    }
                }
            }
        }

        return products;
    }
    catch (Exception ex)
    {
        // Log or handle the exception appropriately
        throw new Exception($"An error occurred while retrieving all products: {ex.Message}");
    }
}

    }
}
