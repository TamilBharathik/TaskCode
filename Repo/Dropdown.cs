using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using backendtask.Model;

namespace backendtask.Repo
{
    public class DropdownRepository : IDropdownRepository
    {
        private readonly string? _connectionString;

        public DropdownRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<IEnumerable<Dropdown>> GetAll()
        {
            using IDbConnection dbConnection = new OracleConnection(_connectionString);
            string query = "SELECT * FROM dropdown";
            return await dbConnection.QueryAsync<Dropdown>(query);
        }

        //         public async Task<IEnumerable<Dropdown>> Getcurrency()
        // {
        //     using IDbConnection dbConnection = new OracleConnection(_connectionString);
        //     string query = "SELECT currency FROM dropdown";
        //     return await dbConnection.QueryAsync<Dropdown>(query);
        // }
    }
    }
