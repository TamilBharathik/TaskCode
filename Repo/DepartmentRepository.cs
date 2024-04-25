using System.Collections.Generic;
using System.Data;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using backendtask.Model;

namespace backendtask.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly string _connectionString;

        public DepartmentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<int> GetAllDepartmentIds()
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<int>("SELECT DepartmentID FROM DepartmentIDsTB");
            }
        }

        public IEnumerable<Department> GetDepartmentNamesForId(int departmentId)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<Department>("SELECT DepartmentName FROM DepartmentNamesTB WHERE DepartmentID = :DepartmentId", new { DepartmentId = departmentId });
            }
        }
    }
}
