using System.Collections.Generic;
using backendtask.Model;

namespace backendtask.Repositories
{
    public interface IDepartmentRepository
    {
        IEnumerable<int> GetAllDepartmentIds();
        IEnumerable<Department> GetDepartmentNamesForId(int departmentId);
    }
}
