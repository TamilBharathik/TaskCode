using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using backendtask.Model;

namespace backendtask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly string? _connectionString;

        public CountriesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dropdown>>> GetCountries()
        {
            using IDbConnection dbConnection = new OracleConnection(_connectionString);
            string query = "SELECT * FROM dropdown";
            IEnumerable<Dropdown> countries = await dbConnection.QueryAsync<Dropdown>(query);
            return Ok(countries);
        }
    }
}