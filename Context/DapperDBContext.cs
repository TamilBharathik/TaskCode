using Oracle.ManagedDataAccess.Client;
using System.Data;
using Microsoft.Extensions.Configuration;
using System;

namespace backendtask.Context
{
    public class DapperDBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperDBContext(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connectionString = _configuration.GetConnectionString("Default") ?? throw new ArgumentNullException("Connection string is null.");
        }

    public object SubmissionDates { get; internal set; }

    public IDbConnection CreateConnection() => new OracleConnection(_connectionString);
    }
}
